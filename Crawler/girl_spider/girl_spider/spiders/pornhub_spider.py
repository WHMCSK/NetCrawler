# -*- coding: utf-8 -*-
import scrapy


class PornhubSpiderSpider(scrapy.Spider):
    name = 'pornhub'
    allowed_domains = ['www.pornhub.com']
    start_urls = [
        'http://www.pornhub.com/view_video.php?viewkey=742377581', 'https://www.pornhub.com/']

    def parse(self, response):
        filename = response.url.split("/")[-2] + '.html'
        print("-----filename: ", filename)
        with open(filename, 'wb') as f:
            f.write(response.body)
