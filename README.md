# RaptorCMS
Made with ASP.NET Core, Raptor is a cross-platform, feature rich and lightweight content management system for your next awesome project!

![Raptor Banner](https://imgur.com/wzzojaD.png)

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

## License

The RaptorCMS is open-sourced software under the [MIT License](http://opensource.org/licenses/MIT)
