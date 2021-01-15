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





CREATE OR REPLACE FUNCTION remove_country(country_id int)
returns void
 AS
    $$
    BEGIN
        delete from countries where id=country_id;
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



