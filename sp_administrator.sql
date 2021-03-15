drop FUNCTION get_administrator

CREATE OR REPLACE FUNCTION get_administrator(administrator_id bigint )
returns TABLE(id bigint,first_name text,last_name text,administrator_level int, user_id bigint )
 AS
    $$
    BEGIN
        RETURN QUERY
        select * from administrators a where a.id = administrator_id ;
       END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_administrators( )
returns TABLE(id bigint,first_name text,last_name text,administrator_level int, user_id bigint )
 AS
    $$
    BEGIN
        RETURN QUERY
         select * from administrators ;
    END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION add_administrator(_first_name text,_last_name text,_administrator_level int, _user_id bigint )
returns void
 AS
    $$
    BEGIN
        insert into administrators 
       (first_name, last_name, administrator_level ,user_id ) 
      values (_first_name,_last_name,_administrator_level,_user_id );
    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION remove_administrator(administrator_id bigint)
returns void
 AS
    $$
    BEGIN
        delete from administrators where id=administrator_id;
    END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION update_administrator(_administrator_id bigint,_first_name text,_last_name text,_administrator_level int, _user_id bigint )
returns void
 AS
    $$
    BEGIN
        update administrators set first_name=_first_name, last_name=_last_name,
        administrator_level=_administrator_level ,user_id =_user_id
        where id=_administrator_id;
    END;
$$ LANGUAGE plpgsql;
