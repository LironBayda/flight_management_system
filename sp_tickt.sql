CREATE OR REPLACE FUNCTION get_ticket(ticket_id bigint )
returns TABLE(id bigint,flight_id bigint,customer_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from tickets t  where t.id = ticket_id ;
       END;
$$ LANGUAGE plpgsql;



drop function get_tickets( );
CREATE OR REPLACE FUNCTION get_tickets( )
returns TABLE(id bigint,flight_id bigint,customer_id bigint)
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from tickets ;
    END;
$$ LANGUAGE plpgsql;


drop function add_ticket(flight_id bigint,customer_id bigint);
CREATE OR REPLACE FUNCTION add_ticket(_flight_id bigint,_customer_id bigint)
returns void
 AS
    $$
    BEGIN
        insert into tickets (flight_id ,customer_id )
       values (_flight_id ,_customer_id );
    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION remove_tickt(remove_id int)
returns void
 as
    $$    
    begin
	    
	  
         delete from tickets where id =remove_id;     


    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION update_ticket(_id bigint,_flight_id bigint,_customer_id bigint)
returns void
 AS
   $$
    BEGIN
        update tickets 
       set flight_id=_flight_id ,customer_id=_customer_id
      where id=_id;
    END;
$$ LANGUAGE plpgsql;



