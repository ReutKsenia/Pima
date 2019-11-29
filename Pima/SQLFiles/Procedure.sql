----------------------------------------------------Registration----------------------------------------------------------------
--DROP PROCEDURE REGISTRATION;
CREATE OR REPLACE PROCEDURE REGISTRATION (p_login IN "User"."Login"%TYPE, 
                                      p_password IN "User"."Password"%TYPE
                                      ) IS
BEGIN
    INSERT INTO "User"("Login", "Password") 
        VALUES (p_login, p_password);
        COMMIT;
END;
-------------------------------------------------EXAMPLE Register---------------------------------------------------------------
BEGIN
REGISTRATION('qwer', 'asdf');
END;


-------------------------------------------------------Login--------------------------------------------------------------------
--DROP PROCEDURE Login;
CREATE OR REPLACE PROCEDURE Login (p_login IN "User"."Login"%TYPE, 
                                   p_password IN "User"."Password"%TYPE,
                                   p_userid OUT "User"."UserId"%TYPE   
                                      ) IS
BEGIN
    SELECT "User"."UserId" INTO p_userid FROM "User" WHERE "User"."Login" = p_login AND "User"."Password" = p_password;
END LOGIN;
---------------------------------------------------EXAMPLE Login-------------------------------------------------------------
DECLARE
userid "User"."UserId"%TYPE;
BEGIN
LOGIN('admin','admin', userid);
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line(userid);
END;

------------------------------------------------------PersonalInfoUpdate--------------------------------------------------------
--DROP PROCEDURE PERSONAINFOUPDATE;
CREATE OR REPLACE PROCEDURE PERSONAINFOUPDATE(p_userid IN "User"."UserId"%TYPE,
                                              p_login IN "User"."Login"%TYPE, 
                                              p_password IN "User"."Password"%TYPE,
                                              p_foto IN "User"."Foto"%TYPE
                                              ) IS
BEGIN
UPDATE "User" SET "User"."Login" = p_login, "User"."Password" = p_password, "User"."Foto" = p_foto
             WHERE "User"."UserId" = p_userid;
COMMIT;
END;

-------------------------------------------------------Show Articles------------------------------------------------------------
--DROP PROCEDURE SHOWARTICLE;
CREATE OR REPLACE PROCEDURE SHOWARTICLE(p_articleid OUT "Article"."ArticleId"%TYPE, p_title OUT "Article"."Title"%TYPE,
                                        p_text OUT "Article"."Text"%TYPE, p_image OUT "Article"."Image"%TYPE
                                        ) IS
BEGIN
FOR rec IN (
SELECT "Article"."ArticleId", "Article"."Title", "Article"."Text", "Article"."Image"
INTO p_articleid, p_title, p_text, p_image
FROM "Article")
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."ArticleId" || rec."Title" || rec."Text");
END LOOP;
END SHOWARTICLE;
-------------------------------------------------EXAMPLE SHOWCATALOG------------------------------------------------------------
DECLARE
productid "PRODUCT"."ProductID"%TYPE;
name "PRODUCT"."Name"%TYPE;
manufacturer "PRODUCT"."Manufacturer"%TYPE;
price "PRODUCT"."Price"%TYPE;
photo "PRODUCT"."Photo"%TYPE;
BEGIN
SHOWCATALOG(productid, name, manufacturer, price, photo);
END;

----------------------------------------------------------ArticleUpdate---------------------------------------------------------
--DROP PROCEDURE ARTICLEUPDATE;
CREATE OR REPLACE PROCEDURE ARTICLEUPDATE(p_artid IN "Article"."ArticleId"%TYPE,
                                              p_title IN "Article"."Title"%TYPE, 
                                              p_text IN "Article"."Text"%TYPE,
                                              p_image IN "Article"."Image"%TYPE
                                              ) IS
BEGIN
UPDATE "Article" SET "Article"."Title" = p_title, "Article"."Text" = p_text, "Article"."Image" = p_image
             WHERE "Article"."ArticleId" = p_artid;
COMMIT;
END;

----------------------------------------------------------ArticleDelete---------------------------------------------------------
--DROP PROCEDURE ARTICLEDELETE;
CREATE OR REPLACE PROCEDURE ARTICLEDELETE(p_artid IN "Article"."ArticleId"%TYPE) IS
BEGIN
DELETE FROM "Article" WHERE "Article"."ArticleId" = p_artid;
DELETE FROM "ArticlesUser" WHERE "ArticlesUser"."ArticleId_ArticlesUser"=p_artid;
COMMIT;
END;











