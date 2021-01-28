CREATE OR REPLACE FUNCTION get_airline_company_by_Id(airline_company_id bigint)
returns TABLE(id bigint,airline_company_name text,country_id int, user_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from airline_companies ac   where ac.id = airline_company_id ;
       END;
$$ LANGUAGE plpgsql;


drop function  get_airline_company_by_country;
CREATE OR REPLACE FUNCTION get_airline_company_by_country(_country_id int )
returns TABLE(id bigint,airline_company_name text,country_id int, user_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from airline_companies ac  where ac.country_id = _country_id ;
       END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION get_airline_company_by_username(_username text )
returns TABLE(id bigint,airline_company_name text,country_id int, user_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select ac.id, ac.airline_company_name ,ac.country_id , ac.user_id from airline_companies ac join users u on ac.user_id =u.id 
       where username = _username ;
       END;
$$ LANGUAGE plpgsql;


drop function get_airline_companies( );
CREATE OR REPLACE FUNCTION get_airline_companies( )
returns TABLE(id bigint,airline_company_name text,country_id int, user_id bigint)
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from airline_companies ;
    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION add_airline_company(_airline_company_name text,_country_id bigint, _user_id bigint)
returns void
 AS
    $$
    BEGIN
        insert into airline_companies (airline_company_name ,country_id , user_id ) 
       values (_airline_company_name ,_country_id , _user_id ) ;
    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION remove_airline_company(remove_id bigint)
returns void
 as
    $$    
    begin
	    
	  
         delete from tickets where flight_id in (select id from flights where airline_company_id=remove_id);
             
       delete from flights where airline_company_id =remove_id;
     
      delete from airline_companies where id=remove_id;


    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION update_airline_company(_id bigint,_airline_company_name text,_country_id bigint, _user_id bigint)
returns void
 AS
   $$
    BEGIN
        update airline_companies 
       set airline_company_name=_airline_company_name ,country_id=_country_id, user_id=_user_id
      where id=_id;
    END;
$$ LANGUAGE plpgsql;
