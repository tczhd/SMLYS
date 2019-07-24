GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[InvoiceDisplayIdUpdate] ON [dbo].[Invoice]
AFTER INSERT
AS
     DECLARE @clinicId INT;
     SELECT @clinicId = ClinicId
     FROM inserted;
     SET NOCOUNT ON;
     DECLARE @ID INT;
     DECLARE InvoiceCursor CURSOR
     FOR
         SELECT Id
         FROM Invoice
         WHERE ClinicId = @clinicId 
		 AND (DisplayId IS NULL OR DisplayId<0)
         ORDER BY Id;
     OPEN InvoiceCursor;
     FETCH NEXT FROM InvoiceCursor INTO @ID;
     WHILE @@FETCH_STATUS = 0
         BEGIN
             DECLARE @displayId INT;
             SELECT @displayId = (SELECT TOP 1 ISNULL(MAX (DisplayId) +1,1)
										FROM Invoice
                                        WHERE ClinicId = @clinicId );
             UPDATE Invoice
               SET
                   DisplayId = @displayId
             WHERE Id = @ID;
             FETCH NEXT FROM InvoiceCursor INTO @ID;
         END;
     CLOSE InvoiceCursor;
     DEALLOCATE InvoiceCursor;

GO

ALTER TABLE [dbo].[Invoice] ENABLE TRIGGER [InvoiceDisplayIdUpdate]
GO


