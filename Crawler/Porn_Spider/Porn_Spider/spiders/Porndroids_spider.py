# -*- coding: utf-8 -*-
import scrapy


class PorndroidsSpiderSpider(scrapy.Spider):
    name = 'Porndroids_spider'
    allowed_domains = ['www.porndroids.com']
    start_urls = ['http://www.porndroids.com/',
                  'https://www.porndroids.com/top-rated/?page=2']

    def parse(self, response):
        pass
