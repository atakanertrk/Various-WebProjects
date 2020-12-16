﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecimServerApi.Operations
{
    public static class Keys
    {
        public static string PrivKey { get; } = "<?xml version=\"1.0\" encoding=\"utf-16\"?> <RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"> <D>AeXlM/kyTfqy25z/MmSc0TBiSB/TQwvIqA3RoC/u/evYc1B4VgpQnPZ/pB3Wd3zT/PTpPC9lvxc1mXorsRfOHLQdx5fZZb/11WAYFV5u7dpN62uohOMiEX3AlaygOenbdRwarsePgPN5kQBdMMh8POr8/OyUcdCxncKzRN2ukRSdL/tLD1Qq+3uffIIVmqqrUtgNtWukWpLdBQATrcjNe/4Vn3i8HbO7WnDoBPDD5K+zl4Jlj4lCSW7XCLLmnlfAZvw8ChLV1XGzpGYz/9Hq3moD9HPxAo1+3foVmr/Gz/rcoENb5L4CmSoHWhrwdIn89p/gxgbVEPKx6NaoOrdVAQ==</D> <DP>WxmeBrUdRGkp1RK3UUTXKSH195LeijF+3EkaSfnuBjyno6uk9O/ixA2uShMhD9l6wXj4ORnh1zuN80IeFk1jY4oGVrhfDvvMQ3AhiMZsMDYXEZPmJjj31rXBcHT1UpICWQDd0JxWxnh+fXTg1ISgmwfm8ASnsFAHXTroTNKRrTk=</DP> <DQ>4U7Fj18s4oRTBvvNAZdARi/V65pqCA3hNVLA1SjZGNaQv4PXZHOfic1o0taxh5P8X5zWb3DgAhHlhsCkxPWw3/JSyUvQ6IUEl0fEULGA/7yMILNp8lXAuvvRh4f9ti5DtfvP50vXO9NBswmEXBfxrxnUQ+icM9p/0z0zkJguboE=</DQ> <Exponent>AQAB</Exponent> <InverseQ>EsJ8uxQe9uZng25djt1ADNHYRaSVuD1HNr9DULIhx7I71Kx+YQrRON7sqypUjrO8qzFJf+zp7V9gILr90wx6nfYVXGvSWpVe+LI3/839LaurTDNqsCPGdbTekEPwq8Sqyf+1+/1up8wwHQ0IU62TGuvvW8BeQhMfzlmiEMPqiTg=</InverseQ> <Modulus>xIhY0IVbKc5bmAzsinuBfokhzi2Sgpqs/UY4G6wdwaVgPdoDCacbllz7toXEu4EEC1eWW2B++DXmcxCuM6BW4LAyf7hKJQJYT1y46f37jQ1Ix6MQMnnI9LBhEl945PaWVWWVDr73Vmc7vHu6x55qewzResFBE7sCo7T0PUk9S+r7kB40Ghj7yo9nQHVPREKAWLmvxZA8t1gn51nUNVvXoEkWjpG/QyJBiHiw3PVq5i+7E8xLMyvLpzBNSS27JkBuTyRcp+F3UsjR/cxMnkPWZFX8g9tcRVvT6EI9VjAtha5D6IUR2Z6Hm5gEkplnkD+3/A92YiREuTD8LpMhWwZPCQ==</Modulus> <P>0vJcfzNnAkrSfzIIA4LJA+026GjCjIqK20ScgYarsXpHJpakBuG96rZQTJHMFzbc/tlLNaowlOSYakwUlxUipEBhjDsPy7Wt/cdL+hovVD0HbEShfXkV7mBwW89j6mUBKGo2qcHthPau7e50kPMs4xsbhezgUv6BMRbsvaUVm0k=</P> <Q>7oHikUIvua0j6f8sRoL9M8/8AvIOHnM1ENRVrXSCgjgYG4qxXHJw8SSBtXOb2922S6Sfy6GNtAn9EzhofZ2xaHS38PnMXPJu89YRru2BwRZz4qfo7wluqjNGzCDN4lOvM9OY98DxYqn5B5BaAGXxuM8poWTe2OKuaAquE2rFVcE=</Q> </RSAParameters>";
        public static string PubKey { get; } = "<?xml version=\"1.0\" encoding=\"utf-16\"?> <RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"> <Exponent>AQAB</Exponent> <Modulus>xIhY0IVbKc5bmAzsinuBfokhzi2Sgpqs/UY4G6wdwaVgPdoDCacbllz7toXEu4EEC1eWW2B++DXmcxCuM6BW4LAyf7hKJQJYT1y46f37jQ1Ix6MQMnnI9LBhEl945PaWVWWVDr73Vmc7vHu6x55qewzResFBE7sCo7T0PUk9S+r7kB40Ghj7yo9nQHVPREKAWLmvxZA8t1gn51nUNVvXoEkWjpG/QyJBiHiw3PVq5i+7E8xLMyvLpzBNSS27JkBuTyRcp+F3UsjR/cxMnkPWZFX8g9tcRVvT6EI9VjAtha5D6IUR2Z6Hm5gEkplnkD+3/A92YiREuTD8LpMhWwZPCQ==</Modulus> </RSAParameters>";
    }
}