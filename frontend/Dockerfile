# Etapa final: Nginx servindo arquivos estáticos
FROM nginx:1.25-alpine

# Apaga config padrão
RUN rm /etc/nginx/conf.d/default.conf

# Copia nossos arquivos estáticos
COPY public /usr/share/nginx/html

# Copia config customizada do Nginx
COPY nginx.conf /etc/nginx/conf.d

# Recebe URL do backend como build-arg
ARG BACKEND_URL
ENV BACKEND_URL=$BACKEND_URL

# Sobrescreve CMD padrão para gerar config.js dinamicamente e iniciar o Nginx
CMD echo "window.APP_CONFIG = { BACKEND_URL: '${BACKEND_URL}' };" > /usr/share/nginx/html/config.js && \
    nginx -g 'daemon off;'

EXPOSE 80
