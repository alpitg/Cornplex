
## docker
```
docker build -t cornplex:5000 .
docker tag cornplex:5000 alpitg/cornplex-api:5000
docker run -d -p 5000:80 --name cornplexcontainer cornplex
```

# kubectl
## kube dashboard
- [ingress concept](https://kubernetes.io/docs/concepts/services-networking/ingress/)
- kubectl config get-contexts
- set KUBECONFIG %UserProfile%\.kube\config

## helm
- [install helm](https://github.com/helm/helm/releases)
- Set the helm path in environment variable.
- minikube start  # if not started for local
- kubectl config get-contexts

- az aks get-credentials --resource-group myResourceGroup --name myAKSCluster  # important for 'helm install' to connect kaas

- helm lint ./cornplex-api-chart    # validate the chart file
- helm install --dry-run --debug ./cornplex-api-chart --generate-name   # minikube start - if got error
- # helm install sample ./cornplex-api-chart --set service.type=NodePort
- helm upgrade --install cornplex-app-release . --namespace=local --set image.tag="1.3" --debug --dry-run
- helm install cornplex-app-api . --set image.tag="1.3" 
