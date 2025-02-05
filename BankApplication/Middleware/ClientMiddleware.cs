﻿using BankApplication.DataBase;
using BankApplication.Interfaces;
using BankApplication.Services;

namespace BankApplication.Middleware
{
    public class ClientMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICreditCardService creditCardService)
        {
            if (context.User.Identity!.IsAuthenticated)
            {
                var clientId = context.User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (clientId != null && context.Request.Path == "/Credit_card")
                {
                    var creditCard = creditCardService.GetCreditCardForClient(int.Parse(clientId));

                    if (creditCard != null)
                    {
                        string html = """
                        <html>
                        <head>
                            <title> Client credit card</title>
                        </head>
                        <style>
                            body {
                                display: flex;
                            }
                            #credit-card {
                                background: rgb(95,9,121);
                                background: linear-gradient(90deg, rgba(95,9,121,1) 9%, rgba(162,0,255,1) 60%, rgba(88,27,75,1) 99%);

                                border-radius: 25px;

                                width: 50vh;
                                height: 30vh;

                                margin: auto;

                                position: relative;

                                padding: 30px;

                                cursor: pointer;
                            }
                            div > a {
                                color: #E8E8EA;
                            }
                            #card-number {
                                width: 80%;
                                height: 20%;

                                position: absolute;

                                top: 55%;
                                left: 20%;
                                transform: translateY(-40%, -20%);
                            }
                            #card-number > a {
                                align-items: center;
                                position: inherit;

                                font-size: 36px;
                            }
                            #expiration-date {
                                position: absolute;
                                top: 85%;
                                margin-left: 5%;

                            }
                            #expiration-date {
                                font-size: 24px;
                            }
                            #fortune {
                                position: absolute;
                                right: 5%;
                                top: 85%;
                                margin-right: 5%;
                            }
                            #fortune > a {
                                font-size: 24px;
                            }
                            #cvv-code {
                                position: absolute;

                                top: 45%;
                                left: 40%;
                                transform: translateY(-60%, -60%);

                                display: none;
                            }
                            #cvv-code > a {
                                font-size: 48px;
                            }
                        </style>
                        <body>
                        """;
                        html += $"""
                            <h1>Your credit card</h1>
                            <div id="credit-card">
                                <div id="card-number"><a>{creditCard.CardNumber}</a></div>
                                <div id="expiration-date"><a>{creditCard.ExpirationDate.Month}/{creditCard.ExpirationDate.Year.ToString().Substring(2)}</a></div>
                                <div id="cvv-code"><a>{creditCard.CVVcode}</a></div>
                                <div id="fortune"><a>{creditCard.Fortune}$</a></div>
                            </div>
                            """;
                        html += """
                            <script>
                                const cardNumber = document.getElementById('card-number');
                                const expirationDate = document.getElementById('expiration-date');
                                const cvvCode = document.getElementById('cvv-code');
                                const fortune = document.getElementById('fortune');

                                document.getElementById('credit-card').addEventListener('click', () => {
                                    if (cardNumber.style.display !== 'none') {
                                        cardNumber.style.display = 'none';
                                        expirationDate.style.display = 'none';
                                        fortune.style.display = 'none'
                                        cvvCode.style.display = 'block';
                                    } else {
                                        cardNumber.style.display = 'block';
                                        expirationDate.style.display = 'block';
                                        fortune.style.display = 'block'
                                        cvvCode.style.display = 'none';
                                    }
                                })
                            </script>
                        </body>
                    </html>
                    """;
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync(html);
                    }
                }
            }
            await _next(context);
        }
    }
}
