CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertClient`(IN c_id INT, c_Name VARCHAR(50), c_PhoneNumber VARCHAR(50), c_Email VARCHAR(50), c_Regdate DATETIME)
BEGIN
    DECLARE emailAcount INT;
    
    SELECT COUNT(*) INTO emailAcount FROM user WHERE Email = c_Email;
	
    IF emailAcount = 0 THEN
        INSERT INTO user(id, Name, PhoneNumber, Email, RegDate) VALUES (c_id, c_Name, c_PhoneNumber, c_Email, c_Regdate);
    ELSE
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El correo electrónico ya está en uso.';
    END IF;
END