
CREATE TABLE countries (
	id   BIGSERIAL   PRIMARY KEY,
	country_name TEXT
);

CREATE TABLE user_roles (
	id   BIGSERIAL  PRIMARY KEY,
	role_name TEXT unique
);

CREATE TABLE users (
	id   BIGSERIAL  PRIMARY KEY   ,
	username text unique,
	user_password text,
	email text unique,
	user_role int,
    FOREIGN KEY (user_role) REFERENCES user_roles(id)

);



CREATE TABLE airline_companies (
	id   BIGSERIAL  PRIMARY KEY   ,
	airline_company_name text unique ,
	country_id INT,
	user_id bigint unique,

    FOREIGN KEY (country_id) REFERENCES countries(id),
    FOREIGN KEY (user_id) REFERENCES users(id)

);
CREATE TABLE flights (
	id  BIGSERIAL  PRIMARY KEY   ,
	airline_company_id bigint,
	origin_country_id INT,
	destination_country_id INT,
	departure_time timestamp ,
	landing_time timestamp,
	remaining_tickets INT,
    FOREIGN KEY (destination_country_id) REFERENCES countries(id),
    FOREIGN KEY (origin_country_id) REFERENCES countries(id),
    FOREIGN KEY (airline_company_id) REFERENCES airline_companies(id)

);

CREATE TABLE customers (
	id   BIGSERIAL  PRIMARY KEY   ,
	first_name text,
	last_name text,
	address text,
	phone_no text unique ,
	credit_card_no text unique,
	user_id bigint unique,
    FOREIGN KEY (user_id) REFERENCES users(id)

);

CREATE TABLE tickets (
	id  BIGSERIAL  PRIMARY KEY,
	flight_id bigint,
	customer_id bigint,
	unique (customer_id,flight_id),
	FOREIGN KEY (flight_id) REFERENCES flights(id),
    FOREIGN KEY (customer_id) REFERENCES customers(id)

);

CREATE TABLE administrators (
	id  BIGSERIAL  PRIMARY KEY,
	first_name text,
	last_name text,
	administrator_level int,
	user_id bigint unique,
	FOREIGN KEY (user_id) REFERENCES users(id)

);
