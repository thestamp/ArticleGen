# Container Deployment Guide

This document explains how to deploy ArticleGen using containers for affordable Azure hosting.

## Quick Start with Docker

### Building the Container Image

```bash
# Build the container image
docker build -f ArticleGen.Web/Dockerfile -t articlegen-web .

# Run locally for testing
docker run -p 8080:8080 -p 8081:8081 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e GptClientSettings__ApiKey=your-openai-api-key \
  articlegen-web
```

### Using Docker Compose

For development and testing:

```bash
# Start the application with docker-compose
docker-compose up --build

# Access the application at http://localhost:8080
```

## Azure Container Apps Deployment

Azure Container Apps provides a serverless container platform that automatically scales based on demand, making it very cost-effective.

### Prerequisites

1. Azure CLI installed
2. Container registry (Azure Container Registry recommended)
3. OpenAI API key

### Step 1: Build and Push Image

```bash
# Login to Azure
az login

# Create resource group
az group create --name articlegen-rg --location eastus2

# Create container registry
az acr create --resource-group articlegen-rg --name articlegenregistry --sku Basic --admin-enabled true

# Build and push image
az acr build --registry articlegenregistry --image articlegen-web:latest .
```

### Step 2: Create Container App Environment

```bash
# Create container app environment
az containerapp env create \
  --name articlegen-env \
  --resource-group articlegen-rg \
  --location eastus2
```

### Step 3: Deploy Container App

```bash
# Deploy using the provided configuration
az containerapp create \
  --name articlegen-web \
  --resource-group articlegen-rg \
  --environment articlegen-env \
  --image articlegenregistry.azurecr.io/articlegen-web:latest \
  --target-port 8080 \
  --ingress 'external' \
  --min-replicas 0 \
  --max-replicas 10 \
  --cpu 0.5 \
  --memory 1Gi \
  --secrets gpt-client-settings-apikey=your-openai-api-key \
  --env-vars ASPNETCORE_ENVIRONMENT=Production GptClientSettings__ApiKey=secretref:gpt-client-settings-apikey
```

### Alternative: Using the YAML Configuration

Update the `azure-containerapp.yaml` file with your specific values and deploy:

```bash
az containerapp create --yaml azure-containerapp.yaml
```

## Health Checks

The application now includes health check endpoints that work in production:

- **Liveness probe**: `/alive` - Basic application responsiveness
- **Readiness probe**: `/health` - Full application health including dependencies

These endpoints are essential for:
- Container orchestration platforms (Azure Container Apps, AKS, etc.)
- Load balancers and application gateways
- Monitoring and alerting systems

## Environment Variables

Key environment variables for container deployment:

- `ASPNETCORE_ENVIRONMENT`: Set to `Production` for production deployments
- `ASPNETCORE_URLS`: HTTP URLs the application listens on (default: `http://+:8080`)
- `GptClientSettings__ApiKey`: OpenAI API key for the AI services
- `ASPNETCORE_FORWARDEDHEADERS_ENABLED`: Enables proper handling of forwarded headers from proxies

## Cost Optimization Tips

1. **Use Azure Container Apps**: Scales to zero when not in use
2. **Set appropriate CPU/Memory limits**: Start with 0.5 CPU and 1Gi memory
3. **Configure autoscaling**: Scale based on HTTP requests
4. **Use Azure Container Registry Basic tier**: For most small applications
5. **Monitor costs**: Set up cost alerts in Azure

## Security Considerations

1. Store sensitive configuration in Azure Key Vault or Container App secrets
2. Use managed identity for accessing Azure services
3. Enable HTTPS only in production
4. Regularly update base images for security patches

## Troubleshooting

### Common Issues

1. **Application not starting**: Check logs with `az containerapp logs show`
2. **Health checks failing**: Verify endpoints are accessible on port 8080
3. **API key issues**: Ensure secrets are properly configured

### Viewing Logs

```bash
# View application logs
az containerapp logs show --name articlegen-web --resource-group articlegen-rg

# Follow logs in real-time
az containerapp logs show --name articlegen-web --resource-group articlegen-rg --follow
```

## Next Steps

1. Set up CI/CD pipeline using GitHub Actions
2. Configure custom domain with SSL certificate
3. Set up monitoring and alerting
4. Implement blue-green deployment strategy