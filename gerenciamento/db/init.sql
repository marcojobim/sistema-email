CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE email_schedules (
    id SERIAL PRIMARY KEY,
    destinatario VARCHAR(255) NOT NULL,
    assunto TEXT NOT NULL,
    corpo TEXT,
    data_programada TIMESTAMP NOT NULL,
    is_sent BOOLEAN DEFAULT FALSE
);

CREATE TABLE email_responses (
    id SERIAL PRIMARY KEY,
    email_schedule_id INT NOT NULL,
    status VARCHAR(50) NOT NULL,
    attempts INT NOT NULL DEFAULT 1,
    response_message TEXT,
    FOREIGN KEY (email_schedule_id) REFERENCES email_schedules(id) ON DELETE CASCADE
);

CREATE INDEX idx_email_schedule_data_programada ON email_schedules(data_programada);