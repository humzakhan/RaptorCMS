# RaptorCMS
Made with ASP.NET Core, Raptor is a cross-platform, feature rich and lightweight content management system for your next awesome project!

![Raptor Banner](https://imgur.com/wzzojaD.png)


<p align="center">
  <a href="https://travis-ci.org/humzakhan/RaptorCMS">
    <img src="https://travis-ci.org/humzakhan/RaptorCMS.svg?branch=master" alt="Build" />
  </a>
  <a href="http://opensource.org/licenses/MIT"><img src="https://img.shields.io/github/license/mashape/apistatus.svg" alt="License" /></a>
  <a href="https://github.com/humzakhan/RaptorCMS/releases"><img src="https://img.shields.io/github/release/humzakhan/RaptorCMS.svg?style=flat-square" alt="Release" /></a>
</p>

## Installation

You can deploy the application just like you would deploy a standard .NET Core Web App after. To run locally:

1. Get the files locally using one of the following.
- You can download the zip file form GitHub.
- To clone the git repository, use `git clone https://github.com/humzakhan/RaptorCMS.git`

2. Provide the database connection string to your Postgres server using one of the following:
- Add the connectiong string in the `ConnectionStrings` section in `appconfig.json` as `RaptorDbConnectionString`.
- Add an environment variable of the name `ConnectionStrings:RaptorDbConnectionString` for `Production` environment.

3. After configuring the database, hit `Ctrl + F5` to run the app.

## Demo

Live demo would be added soon.

## Technology

- ASP.NET Core 2.0
- C#
- PostgreSQL
- Entity Framework Core 2.0
- JavaScript, jQuery

## Design

RaptorCMS uses the open source dashboard theme [Modish](https://github.com/humzakhan/Modish) for it's admin panel, and [Balista](https://colorlib.com/) by [Color Lib](https://colorlib.com/) as the default main theme.

## Development Environment

To develop RaptorCMS, we recommend you to use:

- Visual Studio 2017
- ASP.NET Core 2.0

## Contribution

- For issues or suggestions, please start a new thread in the issues section.
- The roadmap is available at our Trello board [RaptorCMS](https://trello.com/b/C1U5X4DB/raptorcms)

## License

The RaptorCMS is open-sourced software under the [MIT License](http://opensource.org/licenses/MIT)
