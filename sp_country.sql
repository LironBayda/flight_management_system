CREATE OR REPLACE FUNCTION get_country(country_id int )
returns TABLE(id_num int,country_name text)
 AS
    $$
    BEGIN
        RETURN QUERY
        select * from countries where id = country_id ;
       END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION get_countries( )
returns TABLE(id_num int,country_name text)
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





CREATE OR REPLACE FUNCTION remove_country(remove_id int)
returns void
 as
    $$
    declare 
             id_of_flight_with_aircompany_that_has_remove_id  int;
            
           id_of_aircompany_with_remove_id int;
    
            id_of_flight_with_remove_id int;
    
    begin
	    
	    select id  into id_of_flight_with_aircompany_that_has_remove_id from  flight  where airline_company_id in 
                   (select id from airline_company 
                        where country_id=remove_id );
         
          select id  into id_of_aircompany_with_remove_id
         from airline_company 
                     where country_id =remove_id;
                     
           select id  into id_of_flight_with_remove_id 
           from flight 
            where origin_country_id =remove_id or destination_country_id =remove_id;
	    
	    delete from  tickets  where flight_id in 
         (id_of_flight_with_aircompany_that_has_remove_id) or 
         flight_id in (id_of_flight_with_remove_id);

	    delete from  flight  where airline_company_id  in
         (id_of_Aircompany_with_remove_id) or 
          origin_country_id =remove_id or 
         destination_country_id =remove_id;
        
       delete  from airline_company where country_id =remove_id;
       delete from countries where id=remove_id;

    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION update_country(name text, country_id int)
returns void
 AS
    $$
    BEGIN
        update countries set country_name=name where id=country_id;
    END;
$$ LANGUAGE plpgsql;



