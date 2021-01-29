CREATE OR REPLACE FUNCTION get_country(country_id bigint )
returns TABLE(id bigint,country_name text)
 AS
    $$
    BEGIN
        RETURN QUERY
        select * from countries c where c.id = country_id ;
       END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_countries( )
returns TABLE(id bigint,country_name text)
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from countries;
    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION add_country(name text )
returns void
 AS
    $$
    BEGIN
        insert into countries (country_name) values (name);
    END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION remove_country(remove_id bigint)
returns void
 as
    $$ 
          
    begin                     
       
       --delete from ticket--
        with 
	
        id_of_flight_with_aircompany_that_has_remove_id as          
      (select id  from  flights  where airline_company_id in 
      (select id from airline_companies where country_id =remove_id)), 
      
       id_of_flight_with_remove_id as  (select id from flights 
      where origin_country_id =remove_id or destination_country_id =remove_id)       
	    
       delete from  tickets  where flight_id in 
       (select * from id_of_flight_with_aircompany_that_has_remove_id) or 
       flight_id in (select * from id_of_flight_with_remove_id);

 --delete from flight--
       delete from  flights  where airline_company_id  in
       (select id from airline_companies where country_id =remove_id) or 
       origin_country_id =remove_id or 
       destination_country_id =remove_id;
--delete from   airline_companies--      
       delete  from airline_companies where country_id =remove_id;      

--delete from   countries--       
       delete from countries where id=remove_id;

    END;
$$ LANGUAGE plpgsql;
       



CREATE OR REPLACE FUNCTION update_country(country_id bigint,name text )
returns void
 AS
    $$
    BEGIN
        update countries set country_name=name where id=country_id;
    END;
$$ LANGUAGE plpgsql;
