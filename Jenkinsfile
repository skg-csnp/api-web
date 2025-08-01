pipeline {
    agent { label 'agent01u' }

    environment {
        HARBOR_REGISTRY = "http://harbor.local/v2/"
        HARBOR_CREDENTIAL = "harbor_jenkins_csnp"
        GITOPS_REPO = "git@github.com:skg-csnp/argo.git"
        GITOPS_CREDENTIAL = "argo-deploy-key"
    }

    stages {
        stage('Setup Environment & Variables') {
            steps {
                script {
                    // Get branch name
                    def BRANCH = env.GIT_BRANCH.tokenize('/').last()

                    // Detect environment
                    def ENV
                    switch (BRANCH) {
                        case 'dev': ENV = 'dev'; break
                        case 'uat': ENV = 'uat'; break
                        case 'pro': ENV = 'pro'; break
                        default:
                            error "❌ Unknown branch ${BRANCH}. Expect dev, uat, or pro."
                    }

                    // Parse service info from JOB_NAME
                    def job = env.JOB_NAME.toLowerCase()
                    def match = job =~ /web-csnp\.([a-z]+)\.(api|job)$/
                    if (!match) {
                        error "❌ Cannot extract service suffix and type from JOB_NAME: ${env.JOB_NAME}"
                    }

                    def SERVICE_SUFFIX = match[0][1]     // e.g. 'identity'
                    def SERVICE_TYPE   = match[0][2]     // 'api' or 'job'
                    def SERVICE        = "${SERVICE_TYPE}-web-${SERVICE_SUFFIX}"
                    def IMAGE_PREFIX   = "harbor.local/csnp/${SERVICE}"
                    def IMAGE_NAME     = "${IMAGE_PREFIX}_${ENV}"
                    def DOCKER_IMAGE   = "${IMAGE_NAME}:${BUILD_NUMBER}"

                    // Export to env
                    env.BRANCH = BRANCH
                    env.ENV = ENV
                    env.SERVICE_SUFFIX = SERVICE_SUFFIX
                    env.SERVICE_TYPE = SERVICE_TYPE
                    env.SERVICE = SERVICE
                    env.IMAGE_PREFIX = IMAGE_PREFIX
                    env.IMAGE_NAME = IMAGE_NAME
                    env.DOCKER_IMAGE = DOCKER_IMAGE

                    echo """
                    =============================
                    Job Name     : ${env.JOB_NAME}
                    Branch       : ${env.BRANCH}
                    Environment  : ${env.ENV}
                    SERVICE TYPE : ${env.SERVICE_TYPE}
                    SERVICE NAME : ${env.SERVICE}
                    Docker Image : ${env.DOCKER_IMAGE}
                    =============================
                    """
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    def capitalSuffix = env.SERVICE_SUFFIX.capitalize()
                    def dockerfilePath = "Csnp.${capitalSuffix}.${env.SERVICE_TYPE.capitalize()}/Dockerfile"

                    def dockerImage = docker.build(
                        env.DOCKER_IMAGE,
                        "-f src/${capitalSuffix}/${dockerfilePath} ."
                    )
                    env.DOCKER_IMAGE_ID = dockerImage.id
                }
            }
        }

        stage('Push to Harbor') {
            steps {
                script {
                    docker.withRegistry(env.HARBOR_REGISTRY, env.HARBOR_CREDENTIAL) {
                        def img = docker.image(env.DOCKER_IMAGE)
                        img.push()
                        img.push('latest')
                    }
                }
            }
        }

        stage('Update GitOps') {
            steps {
                dir('gitops') {
                    deleteDir()
                    sshagent (credentials: [env.GITOPS_CREDENTIAL]) {
                        sh """
                            echo ">>> Clone GitOps Repo"
                            git clone -b ops ${env.GITOPS_REPO} .

                            echo ">>> Update kustomization.yaml"
                            yq e '.images[] |= select(.name == "harbor.local/placeholder").newTag = "${BUILD_NUMBER}"' -i apps/${ENV}/${SERVICE}/kustomization.yaml

                            echo ">>> Git Commit"
                            git config user.email "jenkins@ci.com"
                            git config user.name "Jenkins CI"
                            git add apps/${ENV}/${SERVICE}/kustomization.yaml
                            git commit -m "Update ${SERVICE} image to ${DOCKER_IMAGE}" || echo "No changes"
                            git push origin ops
                        """
                    }
                }
            }
        }
    }

    post {
        success {
            echo "✅ Build and deploy successful: ${env.DOCKER_IMAGE}"
        }
        failure {
            echo "❌ Pipeline failed for ${env.SERVICE} on branch ${env.BRANCH}!"
        }
    }
}
