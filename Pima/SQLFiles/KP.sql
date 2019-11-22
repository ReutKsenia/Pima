ALTER SESSION SET "_ORACLE_SCRIPT"=true;

--DROP TABLESPACE TS_PROGRAMMER;
CREATE TABLESPACE TS_PROGRAMMER
       DATAFILE 'TS_PROGRAMMER.dat'
       SIZE 10M 
       AUTOEXTEND ON;
       
--DROP TABLESPACE TS_PROGRAMMER_TEMP;
CREATE TEMPORARY TABLESPACE TS_PROGRAMMER_TEMP 
        TEMPFILE 'TS_PROGRAMMER_TEMP.dat' 
        SIZE 5M 
        AUTOEXTEND ON;
        
CREATE ROLE RL_PROGRAMMER;
GRANT ALL PRIVILEGES TO RL_PROGRAMMER;

CREATE PROFILE PF_PROGRAMMER LIMIT
       PASSWORD_LIFE_TIME 180      
       SESSIONS_PER_USER 3         
       FAILED_LOGIN_ATTEMPTS 7     
       PASSWORD_LOCK_TIME 1       
       PASSWORD_REUSE_TIME 10      
       PASSWORD_GRACE_TIME DEFAULT 
       CONNECT_TIME 180            
       IDLE_TIME 30; 
       
--DROP USER PROGRAMMER CASCADE;
CREATE USER PROGRAMMER IDENTIFIED BY Pa$$word
    DEFAULT TABLESPACE TS_PROGRAMMER QUOTA UNLIMITED ON TS_PROGRAMMER
    TEMPORARY TABLESPACE TS_PROGRAMMER_TEMP 
    PROFILE PF_PROGRAMMER;


GRANT RL_PROGRAMMER TO PROGRAMMER;