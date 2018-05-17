# -*- coding: utf-8 -*-
import scrapy


class TaommSpiderSpider(scrapy.Spider):
    name = 'taomm'
    allowed_domains = ['taobao.com']
    start_urls = ['https://www.taobao.com/markets/mm/mm2017']

    def parse(self, response):
        print("-----response.url: ", response.url)
        filename = response.url.split("/")[-2] + '.html'
        print("-----filename: ", filename)
        with open(filename, 'wb') as f:
            f.write(response.body)
