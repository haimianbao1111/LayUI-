namespace AlipayDemo
{
	public class Config
	{
        // 应用ID,您的APPID(换成自己的沙箱测试ID)
        public static string AppId = "2016082100302484AV";

        // 支付宝网关
        public static string Gatewayurl = "https://openapi.alipaydev.com/gateway.do";

        // 商户私钥，您的原始格式RSA私钥(换成自己的)
        public static string PrivateKey = "MIIEowIBAAKCAQEA4iVzVbzQEfBBQKgAlpgXPDSKmqVrb6ym2NILrv4kJEYL1uGVKJwz/4A5noyIEBgPPjmA8KJVIsfBl1sT6Gz5xOS1pNYb3DSzjsGm26wpHEMxNLg7dEu+YxYRpVau/+Rl9xnUxUay4OpG7ntteaUpjMtgxmSpNsA0DF/iarFzlsBb5xuFV4BsM8Kcay2OlpSxMoDo+Y8i6tkpkjmgXU+LNyRVY80CDsXQWnJaXd6O90wbhY4ns53ctcdBaOwlfOaRLZYsksKHYyPnXlpFjmyFIZguuEEchVQsFablO80t0/Dxl37TecagWDwyFjGmcNC5xudGveT/qs/asICfakvarwIDAQABAoIBAFG/7giBX8oMStpfoQ7PXh7ic3Rh9guBozLgA183+cKvG5qLI4txpA5pwOqCrLD8sY3x5Z2XYSVYhq/7OaAcvNGjR9KVn66j0oD5miLxAk94LrJ7IurwcpuJH8ngIxTX8NA5o0PJQeKybf/J2JJP1UxTPOGbA9yJ8IbEpqmhCkLlCpycBWY3m1lUU7tms7Noas0jCGGEwhxcOkfOkuCtTK9lT5R8PEvQy9DMdSvrR/SIHKVYVI5e9jwR37xrRNm0JX9p/tKvIAcFjsOlrsnfEeGrc3yAuI08VGvOaYt49gQFcN5joCOaQY8Sf6CTyLtPYQwnHE+ddHQL+TKaWD4w+NkCgYEA8rPtRb3b45mhwTzhO8O23J7s0d7pFMMaRwtEn8Z6L5w7VyzrE9xDz94ic5LpQQ7RMFH9hKLCxBecSEH580k/KuqZs1ixpcBs+KDNVsBrtUC15wXOJ/N6k14vYPB8NAm6kSGOH5UOOSdCmdkeol01Th7i1nvHQI5uhVyNmoGVy4UCgYEA7olOiTpuzRQ8vslCYY/Fp9Leqo3QtbKg+nNpM0xN0R8TZ/DBRFRnky23+PFFSkeG9FlXoG61l/EwBYsPyk3sPbDPmvdJZhdmZjqJSVyvMtR0IowY2GOBVquSTO/4MKlxvrixbdkLuYysHA6+0x1SAIsnQrQ6VoFgmItmFLZMwaMCgYEAlCG9cBOxYfkjAQ5nalLKCV32rSGgoUtzfmkiOWKqsORqU4rW8AhAW175KBFCzLJ+ez+PvKSau5Stjmy2YgcSxt7pcM1xn/kSia2bppdLJhWY8Khaa4fwCIIz6LbfF+PYOzx/wkb3p2qO+9fJGtBI+KVhTge+VWiZjEwxIVx2UQECgYA0u3X5MEm+XHLxWlyqZLY1W5CN87vn7w1XUJw5Eu1BBbWsjLSs/lvHVZwOFIhU22siKX4NoWvToyYSk6Nx/bf0C1pZz8ESKKoif15KMYspmX3zBTWY4KZ/Gn7FM2eSmmLi3sGanxjQdCC4MypMUXcHV7veKymaZg+MqYRvn71oMQKBgFcdZBYQptfS6/zsZBJXylVZQER3g10DTp1dLFoUmYK8Bqqa+l9STEp12HLVev2LDUtqIT2fATOhyNzd1C3Z0+Ro3G4nBWBQ+Qyl44TPgmBp4O0I6mEt6g1q0S64IKP2nk4mALgdKXgEyFdaJ6qFlsXXeaRQ4MOeKZuy8TQCwHVAV+";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。(换成自己的)
        public static string AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA85zpfe34J/CrI/5mL1zpO4sG4339SjRQCZHRr4uMVr93AjmNdvBWPWfN5m/PSHoqiJNw/DY+6kprAACKrgBP6T47JjS9jkRwKSRO/oAMPVg+V4aAzTZIqEjKCyeBUonZnQ6U7Z4B41kOF+lxkdYv6ru3YvLbRsR+p7Xpu806CscZBdh7ITEPM9rC5XXfDTO5NR386FBD74MAwzuyINNcS9oNoQ151eLYpsiSQVxwUYlhXpDLxEIZyxGz7pmzaGYHxgwDeu3//7sNA0EqdEVGCRyP5wtyAkyPEIq9t+A5DFD7kH6TetUT1Qcc9pskQQ5pqmhCNbR8WXmMemfFJnMw+QIDAQABAV";

        // 签名方式
        public static string SignType = "RSA2";

        // 编码格式
        public static string CharSet = "UTF-8";
    }
}