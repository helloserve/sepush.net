# helloserve.SePush (SePush.Net)

This is a .NET Standard 2.0 and .NET 6.0 client for the https://sepush.co.za service. Available on [Nuget](https://www.nuget.org/packages/helloserve.SePush/).

## Usage

Add it:

```
services.AddSePush(config => 
{
    config.Token = "<key from your config or user secrets>";
});
```

or configure it:

```
    "SePushOptions" : {
        "Token": "<key from your config or user secrets"
    }
```

```
services.AddSePush();
```

Inject it:

```
public MyClass(ISePush sePushClient)
{

}
```

Call it:

```
var status = await sePushClient.StatusAsync();
var areas = await sePushclient.AreasSearchAsync("Cape Town");
```

## Remarks

The `ISePush` interface exposes all the functions of the SePush API surface in an easy and async accessible way. Using this package still requires you to request access to obtain a token, and all the same warnings an terms and conditions apply as stated on https://documenter.getpostman.com/view/1296288/UzQuNk3E when using this package.
