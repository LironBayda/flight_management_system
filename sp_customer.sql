CREATE OR REPLACE FUNCTION get_customer_by_Id(customer_id bigint )
returns TABLE(id bigint,first_name text,last_name text,address text,phone_no text,credit_card_no text, user_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from customers c  where c.id = customer_id ;
       END;
$$ LANGUAGE plpgsql;



drop function get_airline_company_by_username;
CREATE OR REPLACE FUNCTION get_customr_by_username(_username text )
returns TABLE(id bigint,first_name text,last_name text,address text,phone_no text,credit_card_no text, user_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select c.id,c.first_name  ,c.last_name, c.address ,c.phone_no ,c.credit_card_no   ,c.user_id from customers c join users u on c.user_id =u.id 
       where username = _username ;
       END;
$$ LANGUAGE plpgsql;


drop function get_customrs( );
CREATE OR REPLACE FUNCTION get_customrs( )
returns TABLE(id bigint,first_name text,last_name text,address text,phone_no text,credit_card_no text, user_id bigint)
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from customers ;
    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION add_customer(_first_name text,_last_name text,_address text,_phone_no text,_credit_card_no text, _user_id bigint)
returns void
 AS
    $$
    BEGIN
        insert into customers (first_name ,last_name ,address ,phone_no ,credit_card_no ,user_id ) 
       values (_first_name ,_last_name ,_address ,_phone_no ,_credit_card_no , _user_id ) ;
    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION remove_customer(remove_id int)
returns void
 as
    $$    
    begin
	    
	  
         delete from tickets where customer_id =remove_id;     
      delete from customers where id=remove_id;


    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION update_customer(_customer_id bigint,_first_name text,_last_name text,_address text,_phone_no text,_credit_card_no text, _user_id bigint)
returns void
 AS
   $$
    BEGIN
        update customers 
       set first_name=_first_name ,last_name=_last_name ,address=_address ,phone_no=_phone_no ,credit_card_no=_credit_card_no ,user_id= _user_id 
      where id=_customer_id;
    END;
$$ LANGUAGE plpgsql;
