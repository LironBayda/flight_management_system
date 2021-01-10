CREATE TABLE countries (
	id  SERIAL PRIMARY KEY,
	name TEXT
);

CREATE TABLE user_roles (
	id  SERIAL PRIMARY KEY,
	role_name TEXT unique
);

CREATE TABLE users (
	id  SERIAL PRIMARY KEY   ,
	username text unique,
	password text,
	email text unique,
	user_role int,
    FOREIGN KEY (user_role) REFERENCES user_roles(id)

);



CREATE TABLE airline_company (
	id  SERIAL PRIMARY KEY   ,
	name text unique ,
	country_id INT,
	user_id INT unique,

    FOREIGN KEY (country_id) REFERENCES countries(id),
    FOREIGN KEY (user_id) REFERENCES users(id)

);
CREATE TABLE flight (
	id  SERIAL PRIMARY KEY   ,
	airline_company_id INT,
	origin_country_id INT,
	destination_country_id INT,
	departure_time date ,
	landing_time date,
	remaining_tickets INT,
    FOREIGN KEY (airline_company_id) REFERENCES countries(id),
    FOREIGN KEY (origin_country_id) REFERENCES countries(id),
    FOREIGN KEY (airline_company_id) REFERENCES airline_company(id)

);


CREATE TABLE customers (
	id  SERIAL PRIMARY KEY   ,
	first_name text,
	last_name text,
	address text,
	phone_no text unique ,
	credit_card_no text unique,
	user_id bigint unique,
    FOREIGN KEY (user_id) REFERENCES users(id)

);

CREATE TABLE tickets (
	id  SERIAL PRIMARY KEY,
	flight_id bigint,
	customer_id bigint,
	unique (customer_id,flight_id),
	FOREIGN KEY (flight_id) REFERENCES flight(id),
    FOREIGN KEY (customer_id) REFERENCES customers(id)

);

CREATE TABLE administrators (
	id  SERIAL PRIMARY KEY,
	first_name text,
	last_name text,
	level int,
	user_id bigint unique,
	FOREIGN KEY (user_id) REFERENCES users(id)

);
