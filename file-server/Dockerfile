FROM nginx:alpine

RUN mkdir -p /usr/share/nginx/html/images && \
    adduser -D -u 1000 myuser && \
    chmod -R myuser:myuser /usr/share/nginx/html

USER myuser

WORKDIR /usr/share/nginx/html

VOLUME ["/usr/share/nginx/html/images"]

EXPOSE 80