# Setup Necessario
1-  Salve o conteudo abaixo em  um arquivo `docker-compose.yml` que inclui Redis, PostgreSQL e RabbitMQ:


```yaml
version: '3.8'

services:
  postgres:
    image: postgres:latest
    container_name: postgres_container
    environment:
      POSTGRES_USER: your_username
      POSTGRES_PASSWORD: your_password
      POSTGRES_DB: your_database
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  redis:
    image: redis:latest
    container_name: redis_container
    ports:
      - "6379:6379"
    volumes:
      - redis_data:/data

  rabbitmq:
    image: rabbitmq:latest
    container_name: rabbitmq_container
    ports:
      - "5672:5672"
      - "15672:15672"
    environment:
      RABBITMQ_DEFAULT_USER: your_username
      RABBITMQ_DEFAULT_PASS: your_password
    volumes:
      - rabbitmq_data:/var/lib/rabbitmq

volumes:
  postgres_data:
  redis_data:
  rabbitmq_data:
```


2-  Execute o comando 
```bash
  docker-compose up -d
```
Este arquivo define três serviços: `postgres`, `redis` e `rabbitmq`. Cada serviço usa a imagem mais recente disponível e mapeia as portas apropriadas do contêiner para o host. Além disso, cada serviço tem um volume dedicado para persistir os dados.



# Base de dados iniciais

1- Criar o database utilizada
```SQL
  create database "teste-ambev";
```

2- Executar o comando abaixo dentro diretorio Ambev.DeveloperEvaluation.WebApi
```BASH
  dotnet ef database update -v 
```

3- Popular algumas tabelas com dados iniciais usando o script abaixo
```SQL

    INSERT INTO public."Users" ("Id", "Username", "Email", "Phone", "Password", "Role", "Status", "CreatedAt", "UpdatedAt") VALUES ('35d6f2bf-b7e1-4513-91c3-800c1e29513c', 'teste', 'teste@uol.com', '19995460517', '$2a$11$K9oScu2pDq/KLIvrqvVExOPgr2Nm1GIZQWwhVkNG9zHVNiHbrA4i.', 'Admin', 'Active', '2025-03-11 18:01:10.593819 +00:00', null);


    INSERT INTO public."Products" ("Id", "Name", "Description", "Price", "Stock") VALUES ('bb343980-5853-4791-a67f-845aebc92420', 'produto 3', 'descricao produto 3', 6.59, 18);
    INSERT INTO public."Products" ("Id", "Name", "Description", "Price", "Stock") VALUES ('17c6ff5e-febe-4157-9e4a-5051f8ea0cfd', 'produto 1', 'descricao produto 1', 2.12, 16);
    INSERT INTO public."Products" ("Id", "Name", "Description", "Price", "Stock") VALUES ('3203b373-332f-4186-ba4c-35f202655299', 'produto 4', 'descricao produto 4', 21.12, 0);
    INSERT INTO public."Products" ("Id", "Name", "Description", "Price", "Stock") VALUES ('c302b74f-a249-4ef1-98e8-3201a01d7963', 'produto 2', 'descricao produto 2', 50.44, 36);

```
# Testes Postman
avaliacao-api-setup/Ambev-tests.postman_collection.json