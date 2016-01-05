# XML-Extractor

[![Travis](https://travis-ci.org/suyati/XML-Extractor.svg?branch=master)](https://travis-ci.org/suyati/XML-Extractor) [![NuGet](http://img.shields.io/nuget/v/XMLExtractor.svg)](https://www.nuget.org/packages/XMLExtractor/) [![Downloads](http://img.shields.io/nuget/dt/XMLExtractor.svg)](https://www.nuget.org/packages/XMLExtractor/)

This nuget package will help you to extract specified contents from an XML. You can map subnodes and attributes of an XML Node to selected properties of a class. 

##Installation

To install XML Extractor, run the following command in the Package Manager Console

```sh
PM> Install-Package XMLExtractor
```

##Quick Start

It is recommended that you install XMLExtractor via NuGet.Or Add a reference to the XMLExtractor.dll

###XML Extarction

1) Create a model class to map the XML.

```csharp
public class XMLClass
{
  [Element(Name="subElement")]
  public string SubElemnt { get; set; }
  
  [Element(Name="number")]
  public int Number { get; set; }

  [Element(Name="nested")]
  public NestedNodeClass Nested { get; set; }
  
  [Property(Name="prop")]
  public string Property { get; set; }
  
  public string NotMaped { get; set; }
}

public class NestedNodeClass
{
  [Value]
  public string Value { get; set; }
  
  [Property]
  public string Property { get; set; }
}
```


* The Element Attribute is specified to map the Sub-Node content with the name specified to the property. (If no name is specified, it will take the property name).

* The Property Attribute is specified to map the attribute of the node with the name specified the property. (If no name is specified, it will take the property name).

* The Value Attribute is specified to map inner Text content of the node to the property.

* If no attribute present among Element,Property and Value, It will be ignored.

2) Call the extension method Extract() to extract from an XML string

```csharp
var myObject=new XMLClass();
myObject.Extract(
  @"<root prop='The property of root Node'>
	    <subElement>The Sub Element</subElement>
	    <nested property="The Property">The Nested Node</nested>
	    <number>123</number>
    </root>"
  );
```

You can also pass an object of XmlNode class to the Extract() method instead of XML string.

###XML Convertion

You can also convert the model to XML Document.

```csharp
XmlDocument xmlDocument = model.ToXml("rootName");
```

You can avoid the root element name, it will take the class name as root name.

##Authors and Contributors

XML Extractor is developed by Suyati Technologies. It is written and maintained by their Product Development team.

####Author:

Sharafudheen KK ([@sharafu_kk](https://twitter.com/sharafu_kk))

##Support or Contact

Have Suggestions? Want to give us something to do? Contact us at : support@suyati.com
