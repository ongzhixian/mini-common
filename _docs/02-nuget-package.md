# Nuget package

```
dotnet pack -p:PackageVersion=2.1.0
```

With Github actions

github.run_id       : A unique number for each run within a repository. This number does not change if you re-run the workflow run.
                    Example of run ids: 1744382636, 1744391767
github.run_number   : A unique number for each run of a particular workflow in a repository. 
                    This number begins at 1 for the workflow's first run, and increments with each new run. 
                    This number does not change if you re-run the workflow run.

```yaml: reference github.run_id and run_number
      - name: Output Run ID
        run: echo ${{ github.run_id }}
      - name: Output Run Number
        run: echo ${{ github.run_number }}
```

```xml:add the following to csproj
<PackageId>AppLogger</PackageId>
<Version>1.0.0</Version>
<Authors>your_name</Authors>
<Company>your_company</Company>
```

```xml:to auto generate package on build, add the following to csproj
<GeneratePackageOnBuild>true</GeneratePackageOnBuild>
```

```xml:Previously, this was a way to version packages
<PackageVersion>$([System.DateTime]::Now.ToString(&quot;yyyy.MM.dd.HHmmss&quot;))</PackageVersion>
```

So for example you can set <VersionPrefix>1.2.3</VersionPrefix> in your csproj and then call `dotnet pack --version-suffix beta1` to produce a 
`YourApp.1.2.3-beta1.nupkg` 
(if you have project reference that you want the version suffix to be applied to as well, 
you need to call dotnet restore /p:VersionSuffix=beta1 before that - this is a known bug in the tooling).



## Publishing to Github Packages

Github Packages is a package hosting service that allows you to publish your packages to a public repository.
(just like Nuget)

```xml:add to csproj
<RepositoryUrl>https://github.com/ongzhixian/mini-common</RepositoryUrl>
```


# Reference

https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli


github context
https://docs.github.com/en/actions/learn-github-actions/contexts#github-context


Github Packages
https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry#publishing-a-package
