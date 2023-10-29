using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCountryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            List<Country> countryList = new List<Country>()
            {
                new Country() { Id = 1,Name = "Croatia", Alpha2 = "HR" , Alpha3 = "HRV",Code = "191" },
                new Country() { Id = 2,Name = "China", Alpha2 = "CN" , Alpha3 = "CHN",Code = "156" },
                new Country() { Id = 3,Name = "Bosnia and Herzegovina", Alpha2 = "BA" , Alpha3 = "BIH",Code = "070" },
                new Country() { Id = 4,Name = "Australia", Alpha2 = "AU" , Alpha3 = "AUS",Code = "036" },
                new Country() { Id = 5,Name = "Finland", Alpha2 = "FI" , Alpha3 = "FIN",Code = "246" },
                new Country() { Id = 6,Name = "Germany", Alpha2 = "DE" , Alpha3 = "DEU",Code = "276" },
                new Country() { Id = 7,Name = "Japan", Alpha2 = "JP" , Alpha3 = "JPN",Code = "392" },
                new Country() { Id = 8,Name = "Mexico", Alpha2 = "MX" , Alpha3 = "MEX",Code = "484" },
                new Country() { Id = 9,Name = "Poland", Alpha2 = "PL" , Alpha3 = "POL",Code = "616" },
                new Country() { Id = 10,Name = "Switzerland", Alpha2 = "CH" , Alpha3 = "CHE",Code = "756" },
                new Country(){Id = 11, Name="Albania", Alpha2="AL",    Alpha3="ALB",   Code="008"},
                new Country(){Id = 12, Name="Algeria",   Alpha2="DZ",    Alpha3="DZA",   Code="012"},
                new Country(){Id = 13, Name="Andorra",   Alpha2="AD",    Alpha3="AND",   Code="020"},
                new Country(){Id = 14, Name="Angola",    Alpha2="AO",    Alpha3="AGO",   Code="024"},
                new Country(){Id = 15, Name="Anguilla",  Alpha2="AI",    Alpha3="AIA",   Code="660"},
                new Country(){Id = 16, Name="Antarctica",    Alpha2="AQ",    Alpha3="ATA",   Code="010"},
                new Country(){Id = 17, Name="Argentina", Alpha2="AR",    Alpha3="ARG",   Code="032"},
                new Country(){Id = 18, Name="Armenia",   Alpha2="AM",    Alpha3="ARM",   Code="051"},
                new Country(){Id = 19, Name="Aruba", Alpha2="AW",    Alpha3="ABW",   Code="533"},
                new Country(){Id = 20, Name="Austria",   Alpha2="AT",    Alpha3="AUT",   Code="040"},
                new Country(){Id = 21, Name="Azerbaijan",    Alpha2="AZ",    Alpha3="AZE",   Code="031"},
                new Country(){Id = 22, Name="Bahamas",   Alpha2="BS",    Alpha3="BHS",   Code="044"},
                new Country(){Id = 23, Name="Bahrain",   Alpha2="BH",    Alpha3="BHR",   Code="048"},
                new Country(){Id = 24, Name="Bangladesh",    Alpha2="BD",    Alpha3="BGD",   Code="050"},
                new Country(){Id = 25, Name="Barbados",  Alpha2="BB",    Alpha3="BRB",   Code="052"},
                new Country(){Id = 26, Name="Belarus",   Alpha2="BY",    Alpha3="BLR",   Code="112"},
                new Country(){Id = 27, Name="Belgium",   Alpha2="BE",    Alpha3="BEL",   Code="056"},
                new Country(){Id = 28, Name="Bermuda",   Alpha2="BM",    Alpha3="BMU",   Code="060"},
                new Country(){Id = 29, Name="Botswana",  Alpha2="BW",    Alpha3="BWA",   Code="072"},
                new Country(){Id = 30, Name="Brazil",    Alpha2="BR",    Alpha3="BRA",   Code="076"},
                new Country(){Id = 31, Name="Bulgaria",  Alpha2="BG",    Alpha3="BGR",   Code="100"},
                new Country(){Id = 32, Name="Burundi",   Alpha2="BI",    Alpha3="BDI",   Code="108"},
                new Country(){Id = 33, Name="Cambodia",  Alpha2="KH",    Alpha3="KHM",   Code="116"},
                new Country(){Id = 34, Name="Cameroon",  Alpha2="CM",    Alpha3="CMR",   Code="120"},
                new Country(){Id = 35, Name="Canada",    Alpha2="CA",    Alpha3="CAN",   Code="124"},
                new Country(){Id = 36, Name="Chad",  Alpha2="TD",    Alpha3="TCD",   Code="148"},
                new Country(){Id = 37, Name="Chile", Alpha2="CL",    Alpha3="CHL",   Code="152"},
                new Country(){Id = 38, Name="Colombia",  Alpha2="CO",    Alpha3="COL",   Code="170"},
                new Country(){Id = 39, Name="Comoros",   Alpha2="KM",    Alpha3="COM",   Code="174"},
                new Country(){Id = 40, Name="Congo", Alpha2="CG",    Alpha3="COG",   Code="178"},
                new Country(){Id = 41, Name="Cuba",  Alpha2="CU",    Alpha3="CUB",   Code="192"},
                new Country(){Id = 42, Name="Czechia",   Alpha2="CZ",    Alpha3="CZE",   Code="203"},
                new Country(){Id = 43, Name="Denmark",   Alpha2="DK",    Alpha3="DNK",   Code="208"},
                new Country(){Id = 44, Name="Ecuador",   Alpha2="EC",    Alpha3="ECU",   Code="218"},
                new Country(){Id = 45, Name="Egypt", Alpha2="EG",    Alpha3="EGY",   Code="818"},
                new Country(){Id = 46, Name="Estonia",   Alpha2="EE",    Alpha3="EST",   Code="233"},
                new Country(){Id = 47, Name="Ethiopia",  Alpha2="ET",    Alpha3="ETH",   Code="231"},
                new Country(){Id = 48, Name="France",    Alpha2="FR",    Alpha3="FRA",   Code="250"},
                new Country(){Id = 49, Name="Gabon", Alpha2="GA",    Alpha3="GAB",   Code="266"},
                new Country(){Id = 50, Name="Gambia",    Alpha2="GM",    Alpha3="GMB",   Code="270"},
                new Country(){Id = 51, Name="Georgia",   Alpha2="GE",    Alpha3="GEO",   Code="268"},
                new Country(){Id = 52 ,Name="Greece"    ,Alpha2="GR"    ,Alpha3="GRC"   ,Code="300"},
                new Country(){Id = 53 ,Name="Greenland"    ,Alpha2="GL"    ,Alpha3="GRL"   ,Code="304"},
                new Country(){Id = 54 ,Name="Grenada"  ,Alpha2="GD"    ,Alpha3="GRD"   ,Code="308"},
                new Country(){Id = 55 ,Name="Guadeloupe"   ,Alpha2="GP"    ,Alpha3="GLP"   ,Code="312"},
                new Country(){Id = 56 ,Name="Guam" ,Alpha2="GU"    ,Alpha3="GUM"   ,Code="316"},
                new Country(){Id = 57 ,Name="Guatemala"    ,Alpha2="GT"    ,Alpha3="GTM"   ,Code="320"},
                new Country(){Id = 58 ,Name="Guernsey" ,Alpha2="GG"    ,Alpha3="GGY"   ,Code="831"},
                new Country(){Id = 59 ,Name="Guinea"   ,Alpha2="GN"    ,Alpha3="GIN"   ,Code="324"},
                new Country(){Id = 60 ,Name="Guyana"   ,Alpha2="GY"    ,Alpha3="GUY"   ,Code="328"},
                new Country(){Id = 61 ,Name="Haiti"    ,Alpha2="HT"    ,Alpha3="HTI"   ,Code="332"},
                new Country(){Id = 62 ,Name="Honduras" ,Alpha2="HN"    ,Alpha3="HND"   ,Code="340"},
                new Country(){Id = 63 ,Name="Hungary"  ,Alpha2="HU"    ,Alpha3="HUN"   ,Code="348"},
                new Country(){Id = 64 ,Name="Iceland"  ,Alpha2="IS"    ,Alpha3="ISL"   ,Code="352"},
                new Country(){Id = 65 ,Name="India"    ,Alpha2="IN"    ,Alpha3="IND"   ,Code="356"},
                new Country(){Id = 66 ,Name="Indonesia"    ,Alpha2="ID"    ,Alpha3="IDN"   ,Code="360"},
                new Country(){Id = 67 ,Name="Iraq" ,Alpha2="IQ"    ,Alpha3="IRQ"   ,Code="368"},
                new Country(){Id = 68 ,Name="Ireland"  ,Alpha2="IE"    ,Alpha3="IRL"   ,Code="372"},
                new Country(){Id = 69 ,Name="Israel"   ,Alpha2="IL"    ,Alpha3="ISR"   ,Code="376"},
                new Country(){Id = 70 ,Name="Italy"    ,Alpha2="IT"    ,Alpha3="ITA"   ,Code="380"},
                new Country(){Id = 71 ,Name="Jamaica"  ,Alpha2="JM"    ,Alpha3="JAM"   ,Code="388"},
                new Country(){Id = 73 ,Name="Jersey"   ,Alpha2="JE"    ,Alpha3="JEY"   ,Code="832"},
                new Country(){Id = 74 ,Name="Jordan"   ,Alpha2="JO"    ,Alpha3="JOR"   ,Code="400"},
                new Country(){Id = 75 ,Name="Kazakhstan"   ,Alpha2="KZ"    ,Alpha3="KAZ"   ,Code="398"},
                new Country(){Id = 76 ,Name="Kenya"    ,Alpha2="KE"    ,Alpha3="KEN"   ,Code="404"},
                new Country(){Id = 77 ,Name="Kiribati" ,Alpha2="KI"    ,Alpha3="KIR"   ,Code="296"},
                new Country(){Id = 78 ,Name="Kuwait"   ,Alpha2="KW"    ,Alpha3="KWT"   ,Code="414"},
                new Country(){Id = 79 ,Name="Kyrgyzstan"   ,Alpha2="KG"    ,Alpha3="KGZ"   ,Code="417"},
                new Country(){Id = 80 ,Name="Latvia"   ,Alpha2="LV"    ,Alpha3="LVA"   ,Code="428"},
                new Country(){Id = 81 ,Name="Lebanon"  ,Alpha2="LB"    ,Alpha3="LBN"   ,Code="422"},
                new Country(){Id = 82 ,Name="Lesotho"  ,Alpha2="LS"    ,Alpha3="LSO"   ,Code="426"},
                new Country(){Id = 83 ,Name="Liberia"  ,Alpha2="LR"    ,Alpha3="LBR"   ,Code="430"},
                new Country(){Id = 84 ,Name="Libya"    ,Alpha2="LY"    ,Alpha3="LBY"   ,Code="434"},
                new Country(){Id = 85 ,Name="Liechtenstein"    ,Alpha2="LI"    ,Alpha3="LIE"   ,Code="438"},
                new Country(){Id = 86 ,Name="Lithuania"    ,Alpha2="LT"    ,Alpha3="LTU"   ,Code="440"},
                new Country(){Id = 87 ,Name="Luxembourg"   ,Alpha2="LU"    ,Alpha3="LUX"   ,Code="442"},
                new Country(){Id = 88 ,Name="Macao"    ,Alpha2="MO"    ,Alpha3="MAC"   ,Code="446"},
                new Country(){Id = 89 ,Name="Madagascar"   ,Alpha2="MG"    ,Alpha3="MDG"   ,Code="450"},
                new Country(){Id = 90 ,Name="Malawi"   ,Alpha2="MW"    ,Alpha3="MWI"   ,Code="454"},
                new Country(){Id = 91 ,Name="Malaysia" ,Alpha2="MY"    ,Alpha3="MYS"   ,Code="458"},
                new Country(){Id = 92 ,Name="Maldives" ,Alpha2="MV"    ,Alpha3="MDV"   ,Code="462"},
                new Country(){Id = 93 ,Name="Mali" ,Alpha2="ML"    ,Alpha3="MLI"   ,Code="466"},
                new Country(){Id = 94 ,Name="Malta"    ,Alpha2="MT"    ,Alpha3="MLT"   ,Code="470"},
                new Country(){Id = 95 ,Name="Martinique"   ,Alpha2="MQ"    ,Alpha3="MTQ"   ,Code="474"},
                new Country(){Id = 96 ,Name="Mauritania"   ,Alpha2="MR"    ,Alpha3="MRT"   ,Code="478"},
                new Country(){Id = 97 ,Name="Mauritius"    ,Alpha2="MU"    ,Alpha3="MUS"   ,Code="480"},
                new Country(){Id = 98 ,Name="Mayotte"  ,Alpha2="YT"    ,Alpha3="MYT"   ,Code="175"},
                new Country(){Id = 100 ,Name="Monaco"   ,Alpha2="MC"    ,Alpha3="MCO"   ,Code="492"},
                new Country(){Id = 101,Name="Mongolia" ,Alpha2="MN"    ,Alpha3="MNG"   ,Code="496"},
                new Country(){Id = 102,Name="Montenegro"   ,Alpha2="ME"    ,Alpha3="MNE"   ,Code="499"},
                new Country(){Id = 103,Name="Montserrat"   ,Alpha2="MS"    ,Alpha3="MSR"   ,Code="500"},
                new Country(){Id = 104,Name="Morocco"  ,Alpha2="MA"    ,Alpha3="MAR"   ,Code="504"},
                new Country(){Id = 105,Name="Mozambique"   ,Alpha2="MZ"    ,Alpha3="MOZ"   ,Code="508"},
                new Country(){Id = 106,Name="Myanmar"  ,Alpha2="MM"    ,Alpha3="MMR"   ,Code="104"},
                new Country(){Id = 107,Name="Namibia"  ,Alpha2="NA"    ,Alpha3="NAM"   ,Code="516"},
                new Country(){Id = 108,Name="Nauru"    ,Alpha2="NR"    ,Alpha3="NRU"   ,Code="520"},
                new Country(){Id = 109,Name="Nepal"    ,Alpha2="NP"    ,Alpha3="NPL"   ,Code="524"},
                new Country(){Id = 110,Name="Nicaragua"    ,Alpha2="NI"    ,Alpha3="NIC"   ,Code="558"},
                new Country(){Id = 111,Name="Niger"    ,Alpha2="NE"    ,Alpha3="NER"   ,Code="562"},
                new Country(){Id = 112,Name="Nigeria"  ,Alpha2="NG"    ,Alpha3="NGA"   ,Code="566"},
                new Country(){Id = 113,Name="Niue" ,Alpha2="NU"    ,Alpha3="NIU"   ,Code="570"},
                new Country(){Id = 114,Name="Norway"   ,Alpha2="NO"    ,Alpha3="NOR"   ,Code="578"},
                new Country(){Id = 115,Name="Oman" ,Alpha2="OM"    ,Alpha3="OMN"   ,Code="512"},
                new Country(){Id = 116,Name="Pakistan" ,Alpha2="PK"    ,Alpha3="PAK"   ,Code="586"},
                new Country(){Id = 117,Name="Palau"    ,Alpha2="PW"    ,Alpha3="PLW"   ,Code="585"},
                new Country(){Id = 118,Name="Panama"   ,Alpha2="PA"    ,Alpha3="PAN"   ,Code="591"},
                new Country(){Id = 119,Name="Paraguay" ,Alpha2="PY"    ,Alpha3="PRY"   ,Code="600"},
                new Country(){Id = 120,Name="Philippines"  ,Alpha2="PH"    ,Alpha3="PHL"   ,Code="608"},
                new Country(){Id = 121,Name="Pitcairn" ,Alpha2="PN"    ,Alpha3="PCN"   ,Code="612"},
                new Country(){Id = 123,Name="Portugal" ,Alpha2="PT"    ,Alpha3="PRT"   ,Code="620"},
                new Country(){Id = 124,Name="Puerto" ,Alpha2="Rico"    ,Alpha3="PR"    ,Code="PRI"},
                new Country(){Id = 125,Name="Qatar"    ,Alpha2="QA"    ,Alpha3="QAT"   ,Code="634"},
                new Country(){Id = 126,Name="Réunion"  ,Alpha2="RE"    ,Alpha3="REU"   ,Code="638"},
                new Country(){Id = 127,Name="Romania"  ,Alpha2="RO"    ,Alpha3="ROU"   ,Code="642"},
                new Country(){Id = 128,Name="Rwanda"   ,Alpha2="RW"    ,Alpha3="RWA"   ,Code="646"},
                new Country(){Id = 129,Name="Samoa"    ,Alpha2="WS"    ,Alpha3="WSM"   ,Code="882"},
                new Country(){Id = 130,Name="Senegal"  ,Alpha2="SN"    ,Alpha3="SEN"   ,Code="686"},
                new Country(){Id = 131,Name="Serbia"   ,Alpha2="RS"    ,Alpha3="SRB"   ,Code="688"},
                new Country(){Id = 132,Name="Seychelles"   ,Alpha2="SC"    ,Alpha3="SYC"   ,Code="690"},
                new Country(){Id = 133,Name="Singapore"    ,Alpha2="SG"    ,Alpha3="SGP"   ,Code="702"},
                new Country(){Id = 134,Name="Slovakia" ,Alpha2="SK"    ,Alpha3="SVK"   ,Code="703"},
                new Country(){Id = 135,Name="Slovenia" ,Alpha2="SI"    ,Alpha3="SVN"   ,Code="705"},
                new Country(){Id = 136,Name="Somalia"  ,Alpha2="SO"    ,Alpha3="SOM"   ,Code="706"},
                new Country(){Id = 137,Name="Spain"    ,Alpha2="ES"    ,Alpha3="ESP"   ,Code="724"},
                new Country(){Id = 138,Name="Sudan"    ,Alpha2="SD"    ,Alpha3="SDN"   ,Code="729"},
                new Country(){Id = 139,Name="Suriname" ,Alpha2="SR"    ,Alpha3="SUR"   ,Code="740"},
                new Country(){Id = 140,Name="Sweden"   ,Alpha2="SE"    ,Alpha3="SWE"   ,Code="752"},
                new Country(){Id = 142,Name="Tajikistan"   ,Alpha2="TJ"    ,Alpha3="TJK"   ,Code="762"},
                new Country(){Id = 143,Name="Thailand" ,Alpha2="TH"    ,Alpha3="THA"   ,Code="764"},
                new Country(){Id = 144,Name="Tokelau"  ,Alpha2="TK"    ,Alpha3="TKL"   ,Code="772"},
                new Country(){Id = 145,Name="Tonga"    ,Alpha2="TO"    ,Alpha3="TON"   ,Code="776"},
                new Country(){Id = 146,Name="Tunisia"  ,Alpha2="TN"    ,Alpha3="TUN"   ,Code="788"},
                new Country(){Id = 147,Name="Türkiye"  ,Alpha2="TR"    ,Alpha3="TUR"   ,Code="792"},
                new Country(){Id = 148,Name="Turkmenistan" ,Alpha2="TM"    ,Alpha3="TKM"   ,Code="795"},
                new Country(){Id = 149,Name="Tuvalu"   ,Alpha2="TV"    ,Alpha3="TUV"   ,Code="798"},
                new Country(){Id = 150,Name="Uganda"   ,Alpha2="UG"    ,Alpha3="UGA"   ,Code="800"},
                new Country(){Id = 151,Name="Ukraine"  ,Alpha2="UA"    ,Alpha3="UKR"   ,Code="804"},
                new Country(){Id = 152,Name="Uruguay"  ,Alpha2="UY"    ,Alpha3="URY"   ,Code="858"},
                new Country(){Id = 153,Name="Uzbekistan"   ,Alpha2="UZ"    ,Alpha3="UZB"   ,Code="860"},
                new Country(){Id = 154,Name="Vanuatu"  ,Alpha2="VU"    ,Alpha3="VUT"   ,Code="548"},
                new Country(){Id = 155,Name="Yemen"    ,Alpha2="YE"    ,Alpha3="YEM"   ,Code="887"},
                new Country(){Id = 156,Name="Zambia"   ,Alpha2="ZM"    ,Alpha3="ZMB"   ,Code="894"},
                new Country(){Id = 157,Name="Zimbabwe" ,Alpha2="ZW"    ,Alpha3="ZWE"   ,Code="716"}
            };



            migrationBuilder.Sql("SET IDENTITY_INSERT Country ON ");

            foreach (var country in countryList)
            {
                migrationBuilder.Sql($"INSERT INTO Country (Id,Name,Alpha2,Alpha3,Code) VALUES (" +
                    $"{country.Id},'{country.Name}','{country.Alpha2}','{country.Alpha3}','{country.Code}')");
            }
            migrationBuilder.Sql("SET IDENTITY_INSERT Country OFF ");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Country");
        }
    }
}
