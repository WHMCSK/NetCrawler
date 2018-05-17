# python包管理之pip，其实很简单！

## 前言
pip 是一个Python包管理工具，主要是用于安装 PyPI 上的软件包，可以替代 easy_install 工具。

## 安装pip

1、源码安装
Python2编译安装的时候没有安装pip，可以直接从官方地址下载就可以了。官方地址：https://pypi.python.org/pypi/pip
下载解压后，进入目录直接运行python安装就可以了
python setup.py install
（PS：Python3编译安装就默认带了pip了）
2、使用包管理软件安装
Linux系统一般都是有自带Python，如果只需要系统自带的Python，直接从系统的包管理器安装可以了。
yum install python-pip
或者
apt-get install python-pip
## pip更新
pip可以自己更新自己
pip install -U pip
## 基本使用
（以django包为例）
1、安装PyPI软件
pip install django
2、查看具体安装文件
pip show --files django
3、查看哪些软件需要更新
pip list --outdated
4、升级软件包
pip install --upgrade django
5、卸载软件包
pip uninstall django
6、安装具体版本软件
pip install django #最新版本
pip install django==1.11.8 # 指定版本
pip install 'django>=1.11.0' # 大于某个版本
7、 Requirements文件安装依赖软件
Requirements文件 一般记录的是依赖软件列表，通过pip可以一次性安装依赖软件包:
pip freeze > requirements.txt
pip install -r requirements.txt
8、 列出软件包清单
pip list
pip list --outdated
9、查看软件包信息
pip show django
10、搜索
pip search django
## 配置pip
配置文件: $HOME/.pip/pip.conf,
比如使用阿里云的同步镜像：
[global]index-url = http://mirrors.aliyun.com/pypi/simple/[install]trusted-host=mirrors.aliyun.com
## 命令行自动补全
对于bash:
pip completion --bash >> ~/.profile
对于zsh:
pip completion --zsh >> ~/.zprofile