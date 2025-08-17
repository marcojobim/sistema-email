CREATE EXTENSION IF NOT EXISTS "pgcrypto";


CREATE TABLE email_schedule (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),  
    recipient VARCHAR(255) NOT NULL, -- To 
    subject TEXT NOT NULL,
    body TEXT,
    attachments JSONB,               
    send_time TIMESTAMP NOT NULL
);

CREATE TABLE email_responses (
    id SERIAL PRIMARY KEY,
    email_id UUID NOT NULL,          
    status VARCHAR(50) NOT NULL,
    attempts INT NOT NULL DEFAULT 1,
    response_message TEXT,           
    FOREIGN KEY (email_id) REFERENCES email_schedule(id) ON DELETE CASCADE
);