# -*- coding: utf-8 -*-
import scrapy


class QqSpiderSpider(scrapy.Spider):
    name = 'qq'
    allowed_domains = ['www.qq.com']
    start_urls = ['http://www.qq.com/']

    def parse(self, response):
        filename = response.url.split("/")[-2] + '.html'
        print("------filename", filename)

        
        self.saveHtmlFile(filename, response)

    def saveHtmlFile(self, filename, response):
        with open(filename, 'wb') as f:
            f.write(response.body)
