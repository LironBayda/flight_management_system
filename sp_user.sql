
CREATE OR REPLACE FUNCTION get_user(user_id bigint )
returns TABLE(id bigint,username text,password text, email text, user_role int)
 AS
    $$
    BEGIN
        RETURN QUERY
        select * from users u  where u.id = user_id ;
       END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_users( )
returns TABLE(id bigint,username text,password text, email text, user_role int)
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from users;
    END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION add_user(_username text,_user_password text, _email text, _user_role int)
returns void
 AS
    $$
    BEGIN
        insert into users ( username ,user_password , email , user_role ) 
       values ( _username ,_user_password , _email ,_user_role);
    END;
$$ LANGUAGE plpgsql;


select  * from add_user ( '_username1' ,'_password1' , '_email1' ,3); 

select  * from users ; 

CREATE OR REPLACE FUNCTION remove_user(remove_id bigint)
returns void
 as
    $$
    declare 

           id_of_aircompany_with_remove_id bigint;
           id_of_customer_with_remove_id bigint ;         
             id_of_flight_with_aircompany_that_has_remove_id  bigint;            
    
    
    begin
         
          select id  into id_of_aircompany_with_remove_id
         from airline_companies 
                     where user_id =remove_id;
                     
            select id  into id_of_customer_with_remove_id
         from customers where user_id =remove_id;
                    
         select id  into id_of_flight_with_aircompany_that_has_remove_id
         from flights where airline_company_id in (id_of_aircompany_with_remove_id);
                    
                     
	    
	    delete from  tickets  where flight_id in 
         (id_of_flight_with_aircompany_that_has_remove_id) or 
         customer_id in (id_of_customer_with_remove_id);

	    delete from  flights  where airline_company_id  in
         (id_of_aircompany_with_remove_id);
      
        
       delete  from airline_companies where user_id =remove_id;
       delete  from customers where user_id =remove_id;
       delete  from administrators where user_id =remove_id;
      delete from  users where id=remove_id;


    END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_user(_user_id bigint,_username text,_user_password text, _email text, _user_role int)
returns void
 AS
   $$
    BEGIN
        update users 
       set username=_username ,user_password=_user_password, email=_email , user_role=_user_role
      where id=_user_id;
    END;
$$ LANGUAGE plpgsql;

