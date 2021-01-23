CREATE OR REPLACE FUNCTION get_airline_company_by_Id(airline_company_id int )
returns TABLE(id_num int,airline_company_name text,country_id int, user_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from airline_companies   where id = airline_company_id ;
       END;
$$ LANGUAGE plpgsql;



drop function  get_airline_company_by_country;
CREATE OR REPLACE FUNCTION get_airline_company_by_country(_country_id int )
returns TABLE(id_num int,airline_company_name text,country_id int, user_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from airline_companies ac  where ac.country_id = _country_id ;
       END;
$$ LANGUAGE plpgsql;



drop function get_airline_company_by_username;
CREATE OR REPLACE FUNCTION get_airline_company_by_username(_username text )
returns TABLE(id_num int,airline_company_name text,country_id int, user_id bigint)
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
returns TABLE(id_num int,airline_company_name text,country_id int, user_id bigint)
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from airline_companies ac ;
    END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION add_airline_companies(_airline_company_name text,_country_id int, _user_id bigint)
returns void
 AS
    $$
    BEGIN
        insert into airline_companies (airline_company_name ,country_id , user_id ) 
       values (_airline_company_name ,_country_id , _user_id ) ;
    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION remove_airline_company(remove_id int)
returns void
 as
    $$    
    begin
	    
	  
         delete from tickets where flight_id in (select id from flight where airline_company_id=remove_id);
             
       delete from flight where airline_company_id =remove_id;
     
      delete from airline_companies where id=remove_id;


    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION update_airline_company(_id_num int,_airline_company_name text,_country_id int, _user_id bigint)
returns void
 AS
   $$
    BEGIN
        update airline_companies 
       set id=_id_num ,airline_company_name=_airline_company_name ,country_id=_country_id, user_id=_user_id
      where id=_id_num;
    END;
$$ LANGUAGE plpgsql;
