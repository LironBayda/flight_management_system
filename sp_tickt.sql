
drop  FUNCTION get_ticket

CREATE OR REPLACE FUNCTION get_ticket(ticket_id bigint )
returns TABLE(id bigint,flight_id bigint,customer_id bigint)
 AS
    $$ 
    BEGIN
        RETURN QUERY
        select * from tickets t  where t.id = ticket_id ;
       END;
$$ LANGUAGE plpgsql;

select  * from get_ticket(6); 


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

select  * from get_tickets(); 

drop function add_ticket(flight_id bigint,customer_id bigint);
CREATE OR REPLACE FUNCTION add_ticket(_flight_id bigint,_customer_id bigint)
returns bool
 AS
    $$
    declare 
    _remaining_tickets int ;
    begin
	    
	    select  remaining_tickets into _remaining_tickets from  flights f where f.id =_flight_id;
	   
	   
	    if _remaining_tickets>0 then 
              insert into tickets (flight_id ,customer_id )
               values (_flight_id ,_customer_id );
      
               update flights set remaining_tickets =remaining_tickets -1 where id=_flight_id;
              return true;
       end if;
       
       return false;
    END;
$$ LANGUAGE plpgsql;


select  * from add_ticket (6,2) ; 

select  * from tickets ; 

select  * from flights f2 ; 


CREATE OR REPLACE FUNCTION remove_tickt(remove_id int)
returns void
 as
    $$   
    begin
	 
           update flights set remaining_tickets =remaining_tickets +1
           where id=(select flight_id from tickets t where t.id=remove_id );
	  
         delete from tickets where id =remove_id;     


    END;
$$ LANGUAGE plpgsql;

select  * from remove_tickt(2); 

select  * from tickets c ; 

delete from countries where id=9;

CREATE OR REPLACE FUNCTION update_ticket(_id bigint,_flight_id bigint,_customer_id bigint)
returns void
AS
   $$
   declare 

   current_flight_id int;

    begin

	    
	    -- if we change flight id we need to change the remaining tickets accordenaly--
	    select flight_id into current_flight_id from tickets t where t.id=_id;
	    
	    if _flight_id <> current_flight_id then 
	  
	    update flights set remaining_tickets =remaining_tickets +1
        where id=current_flight_id;
       
        update flights set remaining_tickets =remaining_tickets -1
        where id=_flight_id;
       
       end if;
	    
	    
        update tickets 
       set flight_id=_flight_id ,customer_id=_customer_id
      where id=_id;
     
    END;
$$ LANGUAGE plpgsql;


select  * from update_ticket (6,6,1); 

select  * from flights c ;
