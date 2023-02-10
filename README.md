# TvMazeScraper

This Application is using 2 calls to get data from TvMaze API (https://www.tvmaze.com/api)

All Shows: https://api.tvmaze.com/shows
Show Cast: https://api.tvmaze.com/shows/{ShowId}/cast

It is build following Clean architecture.

Application is using SQL server, either apply migration from code (on TvMaze.Persistence 'update-database') or run sql script from 
SQL folder (Run in sequence). 3-Enable Snapshot script needs to be run in both cases

There is no Authentication implemented

There are two ways to scrape data one is from API (DirectScraping API call) and other one is by hosted worker. 
Hosted worker is commented out for now (In TvMazeScraper.WebApi/HostedServiceExtensions), it will run in background on apppliaction startup once, if enabled

API can be tested with swagger page (Swagger is taking lot of time to load data, if query fro big collection) but i have included Postman export files it can used as well, it is in Postman folder (Import both files one is envirnment variable and othe calls collection)
