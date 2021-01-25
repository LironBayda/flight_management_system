
CREATE OR REPLACE FUNCTION get_flight(flight_id bigint )
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from  flights f where f.id = flight_id ;
       END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION get_flights_vacancy()
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from  flights f where f.remaining_tickets > 0 ;
       END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_flight_by_origin_country(_origin_country_id bigint )
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from  flights f where f.origin_country_id = _origin_country_id ;
       END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_flight_by_destination_country(_destination_country_id bigint )
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from  flights f where f.destination_country_id = _destination_country_id ;
       END;
$$ LANGUAGE plpgsql;


 CREATE OR REPLACE FUNCTION get_flight_by_depature_time(_departure_time timestamp )
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from  flights f where f.departure_time >= _departure_time ;
         END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION get_flight_by_landing_time(_landing_time timestamp )
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from  flights f where f.landing_time >= _landing_time ;
       END;
$$ LANGUAGE plpgsql;

select  * from get_flight_by_landing_time('02.01.1990 10:10'); 

select  * from flights



CREATE OR REPLACE FUNCTION get_flight_by_customer(_customer_id bigint)
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$ 
    BEGIN
        RETURN QUERY
            select  f.id ,f.airline_company_id , f.origin_country_id ,f.destination_country_id , f.departure_time , f.landing_time ,f.remaining_tickets 
            from tickets t join flights f on t.flight_id =f.id where t.customer_id=_customer_id;
       END;
$$ LANGUAGE plpgsql;

drop function get_flights( );
CREATE OR REPLACE FUNCTION get_flights( )
returns TABLE(id bigint,airline_company_id bigint, origin_country_id int,destination_country_id int, departure_time timestamp, landing_time timestamp,remaining_tickets int)
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from flights ;
    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION add_flight(_airline_company_id bigint, _origin_country_id int,_destination_country_id int, _departure_time timestamp, _landing_time timestamp,_remaining_tickets int)
returns void
 AS
    $$
    BEGIN
        insert into flights (airline_company_id , origin_country_id ,destination_country_id , departure_time , landing_time ,remaining_tickets )
       values (_airline_company_id , _origin_country_id ,_destination_country_id , _departure_time , _landing_time ,_remaining_tickets );

    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION remove_flight(remove_id int)
returns void
 as
    $$    
    begin
	    
	  
         delete from tickets where flight_id =remove_id;    
         delete from flights where id =remove_id;     



    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION update_flight(_id bigint,_airline_company_id bigint, _origin_country_id int,_destination_country_id int, _departure_time timestamp, _landing_time timestamp,_remaining_tickets int)
returns void
 AS
   $$
    BEGIN
        update flights 
       set airline_company_id=_airline_company_id , origin_country_id=_origin_country_id ,
       destination_country_id=_destination_country_id ,
       departure_time=_departure_time , landing_time=_landing_time
      ,remaining_tickets=_remaining_tickets 
      where id=_id;
    END;
$$ LANGUAGE plpgsql;
