CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertBankTransaction`(in b_id int  , b_AccountID int , b_TransactionType int , b_Amount decimal(10,2), b_ExternalAcount int , b_RegDate datetime)
BEGIN
	declare newbalance decimal(10,2);
    
    set @newbalance= b_Amount;
    IF @newbalance >0 then 
		insert into bancktransaction(id, AccountID, TransactionType, Amount, ExternalAcount, RegDate ) values(b_id, b_AccountID, b_TransactionType, b_Amount, b_ExternalAcount, b_RegDate);
    else
		SIGNAL SQLSTATE '45000'
        set message_text= "cuando ingresas dinero no lo puedes ingresar negativo";
	end if;
END