using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegrationService
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
           string serverName= Properties.Settings.Default["serverName"].ToString();
           string databaseName= Properties.Settings.Default["databaseName"].ToString();
           string tableName= Properties.Settings.Default["tableName"].ToString();
           string columnName= Properties.Settings.Default["columnName"].ToString();
           string selectedIndex = Properties.Settings.Default["selectedIndex"].ToString();



            queryText.Text = Properties.Settings.Default["Query"].ToString();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Query"] = queryText.Text.Trim();
            Properties.Settings.Default["selectedIndex"] = metroComboBox1.SelectedIndex.ToString();


            Form3 frm = new Form3();
            this.Hide();
            frm.Show();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedIndex == 0)
            {
                queryText.Text = @"
if exists(select * from sys.databases where name not in ('master','tempdb','model','msdb','ReportServer','ReportServerTempDB'))
begin

SELECT @@SERVERNAME                                                   AS 
       ServerName, 
       @@SERVICENAME 
       InstanceName, 
       ( CASE 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '8%' THEN 
           'SQL2000' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '9%' THEN 
           'SQL2005' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '10.0%' 
         THEN 'SQL2008' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '10.5%' 
         THEN 'SQL2008 R2' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '11%' 
         THEN 
           'SQL2012' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '12%' 
         THEN 
           'SQL2014' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '13%' 
         THEN 
           'SQL2016' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '14%' 
         THEN 
           'SQL2017' 
           WHEN CONVERT(VARCHAR(128), Serverproperty ('productversion')) LIKE 
                '15%' 
         THEN 
           'SQL2019' 
           ELSE 'unknown' 
         END )                                                        AS 
       VersionInf, 
       CONVERT(VARCHAR(128), Serverproperty('ProductLevel'))          AS 
       ProductLevel, 
       CONVERT(VARCHAR(128), Serverproperty('Edition'))               AS Edition 
       , 
       CONVERT(VARCHAR(128), Serverproperty('ProductVersion'))        AS 
       ProductVersion, 
       CONVERT(VARCHAR(10), Connectionproperty('net_transport'))      AS 
       net_transport, 
       CONVERT(VARCHAR(10), Connectionproperty('protocol_type'))      AS 
       protocol_type, 
       CONVERT(VARCHAR(10), Connectionproperty('auth_scheme'))        AS 
       auth_scheme, 
       CONVERT(VARCHAR(30), ISNULL(Connectionproperty('local_net_address'),'Null'))  AS 
       local_net_address, 
       CONVERT(VARCHAR(10), ISNULL(Connectionproperty('local_tcp_port'),'Null'))     AS 
       local_tcp_port, 
       CONVERT(VARCHAR(30), ISNULL(Connectionproperty('client_net_address'),'Null')) AS 
       client_net_address, 
       Db_name(MF.database_id)                                        AS 
       databaseName,
	   db.state_desc,
       (SELECT total_physical_memory_kb / 1024 
        FROM   master.sys.dm_os_sys_memory)                           AS 
       'Physical Memory (MB)', 
       (SELECT CONVERT(INT, value_in_use) 
        FROM   master.sys.configurations 
        WHERE  NAME LIKE '%max server memory%')                       AS 
       'Max Server Memory', 
       (SELECT physical_memory_in_use_kb / 1024 
        FROM   master.sys.dm_os_process_memory)                       AS 
       'SQL Server Memory Usage (MB)', 
       (SELECT available_physical_memory_kb / 1024 
        FROM   master.sys.dm_os_sys_memory)                           AS 
       'Available Memory (MB)', 
       (SELECT 'Min. Deger =' 
               + Cast(cfg.minimum AS NVARCHAR) 
               + ', Max. Deger =' 
               + Cast(cfg.maximum AS NVARCHAR) 
               + ', ConfigValue=' 
               + Cast(cfg.value AS NVARCHAR) 
               + ', RunValue=' 
               + Cast(cfg.value_in_use AS NVARCHAR) 
        FROM   master.sys.configurations AS cfg 
        WHERE  configuration_id = '1538') 
       SERVER_COST_THRESHOLD, 
       (SELECT 'Min. Deger =' 
               + Cast(cfg.minimum AS NVARCHAR) 
               + ', Max. Deger =' 
               + Cast(cfg.maximum AS NVARCHAR) 
               + ', ConfigValue=' 
               + Cast(cfg.value AS NVARCHAR) 
               + ', RunValue=' 
               + Cast(cfg.value_in_use AS NVARCHAR) 
        FROM   master.sys.configurations AS cfg 
        WHERE  configuration_id = '1539') 
       SERVER_MAX_DEGREE, 
       create_date, 
       compatibility_level, 
       collation_name, 
       user_access_desc, 
       recovery_model_desc, 
       physical_name                                                  dbsavefile 
       , 
       (size*8/1024) sizeMB, 
       max_size, 
       growth, 
       CAST(is_percent_growth AS int)is_percent_growth, 
       ISNULL(last_backup_device_name,'NULL')last_backup_device_name, 
       ISNULL(backupsecond, 0)backupsecond, 
       ISNULL(backupsize_gb/1024/1024, 0)backupsize_MB, 
       ISNULL(is_compressed,0)is_compressed, 
       ISNULL(last_db_backup_start_date, 0)last_db_backup_start_date, 
       ISNULL(last_db_backup_finish_date,0)last_db_backup_finish_date, 
       ISNULL(backuptype, 'NULL')backuptype, 
       ISNULL([backup age (hours)],999) [backup age (Days)], 
       Getdate() 
       createdDate 
FROM   sys.master_files AS mf 
       LEFT JOIN(SELECT 
                                     database_name, 
                        (SELECT total_physical_memory_kb / 1024 
                         FROM   master.sys.dm_os_sys_memory)     AS 
                                     'Physical Memory (MB)' 
                                     , 
                        (SELECT 
              CONVERT(INT, value_in_use) 
                         FROM   master.sys.configurations 
                         WHERE  NAME LIKE '%max server memory%') AS 
                                     'Max Server Memory' 
                                     , 
                        (SELECT 
              physical_memory_in_use_kb / 1024 
                         FROM   master.sys.dm_os_process_memory) AS 
                'SQL Server Memory Usage (MB)', 
                        (SELECT available_physical_memory_kb / 1024 
                         FROM   master.sys.dm_os_sys_memory)     AS 
                                     'Available Memory (MB)', 
                        (SELECT 'Min. Deger =' 
                                + Cast(cfg.minimum AS NVARCHAR) 
                                + ', Max. Deger =' 
                                + Cast(cfg.maximum AS NVARCHAR) 
                                + ', ConfigValue=' 
                                + Cast(cfg.value AS NVARCHAR) 
                                + ', RunValue=' 
                                + Cast(cfg.value_in_use AS NVARCHAR) 
                         FROM   master.sys.configurations AS cfg 
                         WHERE  configuration_id = '1538') 
                                     SERVER_COST_THRESHOLD, 
                        (SELECT 'Min. Deger =' 
                                + Cast(cfg.minimum AS NVARCHAR) 
                                + ', Max. Deger =' 
                                + Cast(cfg.maximum AS NVARCHAR) 
                                + ', ConfigValue=' 
                                + Cast(cfg.value AS NVARCHAR) 
                                + ', RunValue=' 
                                + Cast(cfg.value_in_use AS NVARCHAR) 
                         FROM   master.sys.configurations AS cfg 
                         WHERE  configuration_id = '1539') 
                                     SERVER_MAX_DEGREE, 
                        M.physical_device_name 
                                     last_backup_device_name, 
                        QUERY.backupsecond, 
                        QUERY.backupsize_gb, 
                        is_compressed, 
                        QUERY.backup_start_date 
                                     last_db_backup_Start_date, 
                        QUERY.last_db_backup_date 
                                     last_db_backup_finish_date, 
                        QUERY.backuptype, 
                        [backup age (hours)], 
                        Getdate()                                createdDate 
                 FROM   (SELECT database_name, 
                                Max(BC.media_set_id) 
                                        media_set_id, 
                                Max(bc.backup_finish_date) 
                                AS 
                                        last_db_backup_date, 
                                ( CASE 
                                    WHEN bc.type = 'D' THEN 'Full_Backup' 
                                    ELSE 'Log_Backup' 
                                  END ) 
                                        BackupType, 
                                Datediff(dd, Max(bc.backup_finish_date), Getdate 
                                ()) 
                                AS 
                                        [Backup Age (Hours)], 
                                Cast(Max(bc.backup_size) AS 
                                    bigint) 
                                AS 
                                BackupSize_GB, 
                                Cast(Datediff(ss, Max(bc.backup_start_date), 
                                     Max(bc.backup_finish_date)) 
                                     AS 
                                     VARCHAR(10 
                                     )) 
                                AS 
                                        BackupSecond, 
                                Max(bc.backup_start_date) 
                                        backup_start_date 
                         FROM   msdb.dbo.backupset bc 
                         WHERE  bc.type IN ( 'D', 'L' ) 
                         GROUP  BY database_name, 
                                   bc.type) QUERY 
                        JOIN msdb.dbo.backupmediafamily m 
                          ON QUERY.media_set_id = m.media_set_id 
                        INNER JOIN msdb.dbo.backupmediaset bm 
                                ON m.media_set_id = bm.media_set_id)AS QUERY2 
              ON Db_name(mf.database_id) = QUERY2.database_name 
                 AND mf.type_desc = CASE 
                                      WHEN( QUERY2.backuptype ) = 'Full_Backup' 
                                    THEN 
                                      'ROWS' 
                                      WHEN ( QUERY2.backuptype ) = 'Log_Backup' 
                                    THEN 
                                      'LOG' 
                                    END 
       INNER JOIN sys.databases AS db 
               ON db.database_id = mf.database_id and mf.database_id<>2 
end

else 
begin

select 
 @@SERVERNAME ServerName,@@SERVICENAME InstanceName, 'YOK' VersionInf,'YOK' ProductLevel
,'Database Yok'Edition,'Yok' productversion,'Yok'net_transport
,'YOK' protocol_type,'Null'auth_scheme,'Null'local_net_adress,'Null'local_tcp_port,'Null'client_net_adress,'DATABASE YOK' database_name,'Null' dbStatus
,0 memory,0 memory,0 memory,0 memory,'Null','Null',GETDATE(),0
,'Null','Null','Null','Null',0,0,0,0,'Null'backupDeviceName,0,0.0,0,GETDATE() backupStartDate,GETDATE() backupFinishDate,'DB Yok',0,GETDATE()


end";
            }
            else if(metroComboBox1.SelectedIndex==1)
            {
                queryText.Text = @"DECLARE @DB_USers TABLE
(DBName sysname, UserName sysname, LoginType sysname, AssociatedRole varchar(max),create_date datetime,modify_date datetime,sid varbinary(85))
INSERT @DB_USers
EXEC sp_MSforeachdb
'
use [?]
SELECT ''?'' AS DB_Name,
case prin.name when ''dbo'' then prin.name + '' (''+ (select SUSER_SNAME(owner_sid) from master.sys.databases where name =''?'') + '')'' else prin.name end AS UserName,
prin.type_desc AS LoginType,
isnull(USER_NAME(mem.role_principal_id),'''') AS AssociatedRole ,create_date,modify_date,prin.sid as sid
FROM sys.database_principals prin
LEFT OUTER JOIN sys.database_role_members mem ON prin.principal_id=mem.member_principal_id
WHERE prin.sid IS NOT NULL and prin.sid NOT IN (0x00) and
prin.is_fixed_role <> 1 AND prin.name NOT LIKE ''##%'''
SELECT
user1.dbname,username dbUser ,ISNULL(SL.name,'') AS serverLogin,logintype,ISNULL(sysadmin,'')sysadmin
,ISNULL(securityadmin,'')securityadmin,ISNULL(serveradmin,'')serveradmin,Isnull(diskadmin,'')diskadmin
,ISNULL(setupadmin,'')setupadmin,ISNULL(processadmin,'')processadmin,ISNULL(dbcreator,'')dbcreator
,ISNULL(bulkadmin,'')bulkadmin,user1.create_date ,user1.modify_date
,STUFF(
(
SELECT ',' + CONVERT(VARCHAR(500),associatedrole)
FROM @DB_USers user2
WHERE
user1.DBName=user2.DBName AND user1.UserName=user2.UserName
FOR XML PATH('')
)
,1,1,'') AS Permissions_user
FROM @DB_USers user1
LEFT JOIN SYS.syslogins sl on user1.sid=sl.sid
left join sys.server_principals sp on sp.sid=sl.sid
where user1.DBName not in ('master','model','msdb','tempdb')
GROUP BY
user1.dbname,username ,logintype ,user1.create_date ,user1.modify_date,sl.sid,SL.name,diskadmin,sysadmin,securityadmin,serveradmin,setupadmin,processadmin,dbcreator,bulkadmin
ORDER BY user1.DBName,username";
            }
            else if (metroComboBox1.SelectedIndex==2){queryText.Text = @"





SET NOCOUNT ON
declare @check int
set @check = 24-- In hours, based on backup status will be updated, if no full or


declare @CheckTlog int
set @CheckTlog = 4-- In hours, based on tlog backup, status will be updated. 

declare @LastFullBackup datetime
declare @LastDiffBackup datetime
declare @lastTlogBackup datetime
declare @NotBackedupSinceHrs int

declare @NoTLogSince int
declare @status nvarchar(40)
declare @Recovery nvarchar(20)
declare @TlogBkpStatus nvarchar(40)
declare @dbstatus nvarchar(30)
declare @dbsize nvarchar(300)
declare @dbbackupsize nvarchar(300)
declare @dbbackuptime nvarchar(500)
declare @dbcompretionRate nvarchar(50)
declare @dbOutPerSec nvarchar(50)
declare @dbInPerSec nvarchar(50)

declare @FinalAge int
declare @hf int
declare @hd int
declare @hl int
declare @ServerName nvarchar(60)
declare @dbname nvarchar(60)


DECLARE @table1 table(Servername nvarchar(60), DBName nvarchar(60), LastFullBackup datetime,
LastDiffBackup datetime, NotBackedUpSince int, dbstatus nvarchar(30), dbsize nvarchar(300), dbbackupsize nvarchar(300),
dbbackuptime nvarchar(500),dbcompretionRate nvarchar(50),dbInPerSec nvarchar(50),dbOutPerSec nvarchar(50), [Status] nvarchar(40), lastTlogBackup datetime,
[Recovery] varchar(20), NoTLogSince int, TlogBkpStatus nvarchar(40))

declare c1 cursor for Select Distinct convert(varchar(60),@@SERVERNAME) as Servername,
   convert(varchar(60), e.database_name) as DBname,
   (Select  convert(varchar(25), Max(backup_finish_date), 100)
   FROM msdb..backupset a Where a.database_name = e.database_name

and  type = 'D'

Group by a.database_name) Last_FullBackup,
(Select convert(varchar(25), Max(backup_finish_date), 100) From msdb..backupset c
Where c.database_name = e.database_name 
and type = 'I' Group by c.database_name) Last_Diff_Backup, NULL as NotBackedUpSinceHrs, NULL as [DBStatus],
NULL as [Status], (Select convert(varchar(25), Max(backup_finish_date), 100)
From msdb..backupset c Where c.database_name = e.database_name


and type = 'L' Group by c.database_name) Last_Diff_Backup,
convert(varchar(20), convert(sysname, DatabasePropertyEx(e.database_name, 'Recovery'))) as Recovery,
NULL, NULL as TlogBkpStatus From msdb..backupset e WHERE e.database_name not in ('tempdb')
and e.database_name in (Select Distinct name from master..sysdatabases where dbid <> 2)

UNION ALL

SELECT DISTINCT convert(varchar(60),@@SERVERNAME) as Servername,
convert(varchar(60), name) as DBname,NULL, NULL,NULL as NotBackedUpSinceHrs,NULL AS[DBStatus],
NULL as [Status],NULL,convert(varchar(20), convert(sysname, DatabasePropertyEx(name, 'Recovery'))),
NULL,NULL from master..sysdatabases as record

WHERE name NOT IN(SELECT DISTINCT database_name FROM msdb..backupset) 
and dbid<>2 ORDER BY 1,2
OPEN c1

FETCH NEXT FROM c1 INTO @ServerName, @dbname, @LastFullBackup, @LastDiffBackup,
@NotBackedupSinceHrs, @dbstatus,@status, @lastTlogBackup, @Recovery, @NoTLogSince, @TlogBkpStatus


WHILE @@FETCH_STATUS = 0

BEGIN

IF(@LastFullBackup IS NULL)

BEGIN

set @LastFullBackup = '1900-01-01 00:00:00.000'

END

IF(@LastDiffBackup IS NULL)

BEGIN

set @LastDiffBackup = '1900-01-01 00:00:00.000'

END

IF(@lastTlogBackup IS NULL)

BEGIN

set @lastTlogBackup = '1900-01-01 00:00:00.000'

END

select @hf = datediff(hh, @LastFullBackup, GETDATE())

select @hd = datediff(hh, @LastDiffBackup, GETDATE())

select @NoTLogSince = datediff(hh, @lastTlogBackup, GETDATE())

IF(@hf < @hd)

SET @FinalAge = @hf

ELSE

SET @FinalAge = @hd
SET @NotBackedupSinceHrs = @FinalAge

--set @dbstatus = null

set @dbstatus = (select ISNULL(convert(varchar(20), DATABASEPROPERTYEX(@dbname, 'status')),''))
set @dbsize = (select ISNULL(cast(CONVERT( DECIMAL(10,2),SUM(cast(size *8.0/1024   as bigint)))as nvarchar(300))+ ' MB','')  from sys.master_files where db_name(database_id)=@dbname)
set @dbbackupsize = (select ISNULL(cast(CONVERT(DECIMAL(10,2),MAX(compressed_backup_size /1024.0/1024.0))as nvarchar(300))+' MB','') from msdb.dbo.backupset WHERE database_name=@dbname)
set @dbbackuptime = (SELECT  ISNULL(right ('0'+CONVERT(varchar(6), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date))/3600),2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), (DATEDIFF(second,MAX(backup_start_date), MAX(backup_finish_date)) % 3600) / 60), 2)+ ':'+ RIGHT('0' + CONVERT(varchar(2), DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date)) % 60), 2),'') from msdb..backupset WHERE database_name=@dbname)
set @dbcompretionRate = (select ISNULL(CAST(CAST(CONVERT(DECIMAL(10,2),MAX(backup_size)/1024.0/1024.0)/CONVERT(DECIMAL(10,2),MAX(compressed_backup_size)/1024.0/1024.0) AS decimal(10,2))AS nvarchar(30)),'') from msdb.dbo.backupset WHERE database_name=@dbname)

set @dbInPerSec  =(SELECT   ISNULL(cast(SUM(cast(num_of_bytes_written/1024/1024 as bigint)) as nvarchar(50))+ ' MB','') AS DISK_num_of_bytes_written FROM sys.dm_io_virtual_file_stats(NULL, NULL) AS vfs INNER JOIN sys.master_files AS mf WITH (NOLOCK) ON vfs.database_id = mf.database_id AND vfs.file_id = mf.file_id and db_name(mf.database_id)=@dbname)
set @dbOutPerSec=(select ISNULL(CAST(CAST(CONVERT(DECIMAL(10,2),MAX(backup_size)/1024.0/1024.0)/(case when CONVERT(DECIMAL(10,2),DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date)))=0 then 1 else CONVERT(DECIMAL(10,2),DATEDIFF(second, MAX(backup_start_date), MAX(backup_finish_date))) end) AS decimal(10,2))AS nvarchar(30))+' MB','') from msdb.dbo.backupset WHERE database_name=@dbname)


--UPDATE @table1 SET[Status] = 'DB in ' + @dbstatus + ' state' where dbStatus<>'ONLINE'

  --print @dbstatus


--print @dbname


INSERT INTO @table1 values(@ServerName, @dbname, @LastFullBackup, @LastDiffBackup,
@NotBackedupSinceHrs, @dbstatus,@dbsize,@dbbackupsize,@dbbackuptime,@dbcompretionRate,@dbInPerSec, @dbOutPerSec ,@status, @lastTlogBackup, @Recovery, @NoTLogSince, @TlogBkpStatus)

--set @dbstatus = null

UPDATE @table1 SET[Status] = CASE
WHEN NotBackedUpSince <= @check   THEN 'Success'
WHEN NotBackedUpSince > = @check THEN '!!! Failed, Action required !!!!'

END
--Print @dbstatus

UPDATE @table1 SET Status = @dbstatus where dbstatus<>'ONLINE'


UPDATE @table1 SET Status = 'Success'where DBName = 'master' and NotBackedUpSince< = @check + 144
UPDATE @table1 SET TlogBkpStatus = CASE
WHEN NoTLogSince<= @CheckTlog THEN 'Success'
WHEN NoTLogSince>= @CheckTlog THEN '!!! Failed, Action required !!!!'
     END


UPDATE @table1 SET TlogBkpStatus = 'NA' where[Recovery] = 'SIMPLE' OR DBName = 'model'

  --print @dbstatus


FETCH NEXT FROM c1 INTO @ServerName, @dbname, @LastFullBackup, @LastDiffBackup,
@NotBackedupSinceHrs, @dbstatus,@status,
@lastTlogBackup, @Recovery, @NoTLogSince, @TlogBkpStatus
END
UPDATE @table1 SET Status = 'Not in Online',TlogBkpStatus = 'Not in Online' where dbstatus<>'ONLINE'

SELECT Servername as 'SQLInstanceName',DBName as 'DatabaseName',LastFullBackup,
LastDiffBackup,NotBackedUpSince as 'LastBackup_Hrs',dbstatus,dbsize,dbbackupsize,dbbackuptime,dbcompretionRate,dbInPerSec,dbOutPerSec  ,[Status] as 'Backup Status',lastTlogBackup , [Recovery] ,
NoTLogSince,TlogBkpStatus FROM @table1 order by DBName

CLOSE c1
DEALLOCATE c1


"; }
            else if (metroComboBox1.SelectedIndex == 3)
            {
                queryText.Text = @"
         
CREATE TABLE #CPUValues(
[index]        SMALLINT,
[description]  VARCHAR(128),
[server_cores] SMALLINT,
[value]        VARCHAR(5) 
)

CREATE TABLE #MemoryValues(
[index]         SMALLINT,
[description]   VARCHAR(128),
[server_memory] DECIMAL(10,2),
[value]         VARCHAR(64) 
)

INSERT INTO #CPUValues
EXEC xp_msver 'ProcessorCount'

INSERT INTO #MemoryValues 
EXEC xp_msver 'PhysicalMemory'


SELECT 
  cast( SERVERPROPERTY('SERVERNAME')as nvarchar(100)) AS 'instance',
   cast(v.sql_version as nvarchar(100)),
   cast((SELECT SUBSTRING(CONVERT(VARCHAR(255),SERVERPROPERTY('EDITION')),0,CHARINDEX('Edition',CONVERT(VARCHAR(255),SERVERPROPERTY('EDITION')))) + 'Edition')as nvarchar(100)) AS sql_edition,
   cast(SERVERPROPERTY('ProductLevel') as nvarchar(100))  AS 'service_pack_level',
   cast(SERVERPROPERTY('ProductVersion') as nvarchar(100))  AS 'build_number',
   (SELECT DISTINCT cast(local_tcp_port as nvarchar(100)) FROM sys.dm_exec_connections WHERE session_id = @@SPID) AS [port],
   (SELECT cast([value] as nvarchar(100))  FROM sys.configurations WHERE name like '%min server memory%') AS min_server_memory,
   (SELECT cast([value] as nvarchar(100))  FROM sys.configurations WHERE name like '%max server memory%') AS max_server_memory,
   (SELECT cast(ROUND(CONVERT(DECIMAL(10,2),server_memory/1024.0),1) as nvarchar(100))  FROM #MemoryValues) AS server_memory,
   server_cores, 
   (SELECT cast(COUNT(*) as nvarchar(100))  AS 'sql_cores' FROM sys.dm_os_schedulers WHERE status = 'VISIBLE ONLINE') AS sql_cores,
   (SELECT cast([value] as nvarchar(100))  FROM sys.configurations WHERE name like '%degree of parallelism%') AS max_dop,
   (SELECT cast([value]  as nvarchar(100)) FROM sys.configurations WHERE name like '%cost threshold for parallelism%') AS cost_threshold_for_parallelism ,cast(getdate() as datetime) as logtime
FROM #CPUValues
LEFT JOIN (
      SELECt
      CASE 
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '8%'    THEN 'SQL Server 2000'
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '9%'    THEN 'SQL Server 2005'
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '10.0%' THEN 'SQL Server 2008'
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '10.5%' THEN 'SQL Server 2008 R2'
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '11%'   THEN 'SQL Server 2012'
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '12%'   THEN 'SQL Server 2014'
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '13%'   THEN 'SQL Server 2016'     
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '14%'   THEN 'SQL Server 2017'
         WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('PRODUCTVERSION')) like '15%'   THEN 'SQL Server 2019' 
         ELSE 'UNKNOWN'
      END AS sql_version
     ) AS v ON 1 = 1
           "; }


        }
    }
}
