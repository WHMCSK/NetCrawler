# -*- coding: utf-8 -*-
import scrapy
import pyorient
import pyorient.ogm


class WeiboSpider(scrapy.Spider):
    name = 'weibo'
    allowed_domains = ['weibo.com']
    start_urls = ['http://weibo.com/']

    client = pyorient.OrientDB("localhost", 2424)
    session_id = client.connect("admin", "admin")
    
    # allowed_domains = ['girl13.com']
    # start_urls = ['http://www.girl13.com/']
    

    def parse(self, response):
        filename = response.url.split("/")[-2] + '.html'
        print("------filename", filename)

        self.saveHtmlFile(filename, response)

    def saveHtmlFile(self, filename, response):
        with open(filename, 'wb') as f:
            f.write(response.body)
