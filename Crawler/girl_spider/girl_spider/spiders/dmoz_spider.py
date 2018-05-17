import scrapy


class DmozSpider(scrapy.Spider):
    name = "dmoz"
    allowed_domains = ["dmoz.org"]
    start_urls = [
        "http://www.ugirls.com/",
        "http://image.baidu.com/search/index?tn=baiduimage&ipn=r&ct=201326592&cl=2&lm=-1&st=-1&fm=detail&fr=&sf=1&fmq=1466973771267_R&pv=&ic=0&nc=1&z=&se=&showtab=0&fb=0&width=&height=&face=0&istype=2&itg=0&ie=utf-8&word=%E7%BE%8E%E5%A5%B3%E5%A3%81%E7%BA%B8#z=0&pn=&ic=0&st=-1&face=0&s=0&lm=-1",
        "http://onehdwallpaper.com/category/girls/",
        "http://www.zastavki.com/eng/girls/beautyful_girls/",
        "http://www.girl13.com/",
        "https://www.taobao.com/markets/mm/mm2017"
    ]

    def parse(self, response): 
        print("-----response.url: ", response.url)
        filename = response.url.split("/")[-2] + '.html'
        print("-----filename: ", filename)
        with open(filename, 'wb') as f:
            f.write(response.body)
