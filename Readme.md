# Учебный проект Otus
## Работа с docker
- docker login
- docker build --platform linux/amd64 -t mxbooll/homework_2m_9l:latest .
- docker push mxbooll/homework_2m_9l:latest
- docker pull mxbooll/homework_2m_9l:latest
- docker run --name my-homework123 -d -p 8000:80 homework_2m_9l
- curl http://localhost:8000/health

## Запуск minikube в windows
- minikube status
- minikube start --driver=hyperv

## Запуск манифестов k8s
- cd ~\k8s-manifests\
- kubectl apply -f .
- kubectl rollout restart deployment homework-service # перезапуск

## Helm
- cd ~\helm\homework_otus_chart\
- Удалить предыдущую установку
  - helm uninstall homework-service      
- Установите заново
  - helm install homework-service . --set ingress-nginx.enabled=true --set ingress.enabled=true -f secrets.yaml
- Обновить  
  - helm upgrade --install homework-service . --set ingress-nginx.enabled=true --set ingress.enabled=true -f secrets.yaml