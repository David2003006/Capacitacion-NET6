CREATE DEFINER=`root`@`localhost` PROCEDURE `selectClient`(IN clientID INT)
BEGIN
   
    IF clientID IS NULL THEN 
        BEGIN
            SELECT * FROM user;
        END;
    ELSE
        BEGIN
            SELECT * FROM USER WHERE id = clientID;
        END;
    END IF;
END