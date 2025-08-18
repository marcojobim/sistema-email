  > Estrutura do Projeto

1. **`appsettings.json`**:
   - Deve conter as configurações de conexão com o banco de dados, como:

     {
       "ConnectionStrings": {
         "DefaultConnection": "Host=localhost;Database=sistema_email;Username=usuario;Password=senha"
       }
     }


2. **`docker-compose.yml`**:
   - Deve configurar o banco de dados (provavelmente PostgreSQL) e garantir que ele esteja rodando corretamente.

3. **`Program.cs`**:
   - Deve configurar o pipeline da aplicação, incluindo o Entity Framework Core, o serviço `EmailManagementService` e o controlador `EmailManagementController`.

4. **`controllers/EmailManagementController.cs`**:
   - Deve expor os endpoints para gerenciar os e-mails, como o endpoint `POST /api/EmailManagement` para agendar e-mails.

5. **`db/`**:
   - **`AppDbContext.cs`**: Deve configurar o contexto do Entity Framework Core para gerenciar as tabelas `email_schedule` e `email_responses`.
   - **`init.sql`**: Deve conter os scripts para criar as tabelas necessárias no banco de dados.
   - **`scripts/index.sql`**: Deve conter os scripts para criar índices no banco de dados, como no campo `send_time` da tabela `email_schedule`.

6. **`models/`**:
   - **`EmailSchedule.cs`**: Modelo para representar os e-mails agendados.
   - **`EmailResponse.cs`**: Modelo para representar as respostas do envio de e-mails.

7. **`services/EmailManagementService.cs`**:
   - Deve conter a lógica principal para:
     - Salvar e-mails no banco de dados.
     - Monitorar e-mails "vencidos".
     - Enviar JSON para a aplicação de envio.
     - Registrar as respostas no banco de dados.


  > Validação

1. **Banco de Dados**:
   - Certifique-se de que o banco de dados está rodando corretamente com o `docker-compose.yml`.
   - Verifique se as tabelas `email_schedule` e `email_responses` foram criadas corretamente ao executar o script `init.sql`.

2. **Conexão com o Banco**:
   - Teste se a aplicação consegue se conectar ao banco de dados usando a string de conexão em `appsettings.json`.

3. **API**:
   - Teste o endpoint `POST /api/EmailManagement` para verificar se os e-mails estão sendo salvos no banco de dados.

4. **Monitoramento e Envio**:
   - Certifique-se de que o serviço `EmailManagementService` está monitorando o banco de dados e enviando os e-mails "vencidos" para a aplicação de envio.

5. **Resposta da Aplicação de Envio**:
   - Verifique se as respostas da aplicação de envio estão sendo registradas corretamente na tabela `email_responses`.
