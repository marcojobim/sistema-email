# Etapa final: Nginx servindo arquivos estáticos
FROM nginx:1.25-alpine

# Apaga config padrão
RUN rm /etc/nginx/conf.d/default.conf

# Copia nosso index.html e outros estáticos
COPY public /usr/share/nginx/html

# Copia config customizada
COPY nginx.conf /etc/nginx/conf.d

EXPOSE 80
