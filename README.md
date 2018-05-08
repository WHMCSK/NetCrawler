# NetCrawler
网络爬虫

# 下载girl13的美女图片

首先到girl_spider/girl_spider/settings.py下修改图片保存路径

IMAGES_STORE = '/Volumes/olive/scrapy_files/images'

将'/Volumes/olive/scrapy_files/images'修改为你自己的保存路径

然后执行
```
cd girl_spider # 进入/girl_spider目录
scrapy crawl girl13
```

图片就要开始下载了