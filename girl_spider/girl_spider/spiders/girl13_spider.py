# -*- coding: utf-8 -*-
import scrapy
from girl_spider.items import GirlSpiderItem

class Girl13SpiderSpider(scrapy.Spider):
    name = 'girl13'
    allowed_domains = ['girl13.com']
    start_urls = ['http://www.girl13.com/']

    def parse(self, response):
        print("-----response.url: ", response.url)
        filename = response.url.split("/")[-2] + '.html'
        print("-----filename: ", filename)
        with open(filename, 'wb') as f:
            f.write(response.body)
        for category_href in response.selector.xpath('//a[@rel="tag"]/@href'):
            print('_+______category_href', category_href.extract())
            category_url = category_href.extract()
            print("category_url", category_url)
            yield scrapy.Request(category_url, callback=self.parse_question)

    def parse_question(self, response):
        img_urls = response.selector.xpath('//img/@src').extract()
        print('***********thumbnails', img_urls)
        item = GirlSpiderItem()
        del img_urls[0]
        img_urls.pop()
        item['image_urls'] = img_urls
        print("+++++++++++++++item['image_urls']", item['image_urls'])
        yield item
