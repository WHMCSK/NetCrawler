# ISSUES

错误1:

错误信息： 

```
The type initializer for 'MySql.Data.MySqlClient.Replication.ReplicationManager' threw an exception.
```
环境：
* Mac OS
* VS for mac 7.5.1

解决：

经过几个小时尝试，最后发现是因为项目的某个祖先目录是中文名称。把这个中文名称改成英文名称解决。

错误2:

错误信息：

大概诸如Mysql服务器不支持ssl连接

原因：

.Net Core下的MySql.Data.EntityFrameworkCore使用的时候默认使用Ssl连接数据。

解决：
下面方法二选一。
1. 将Mysql数据库配置成支持ssl连接，并设置好证书。
2. 在连接字符串里面加上"SslMode=None;"。

