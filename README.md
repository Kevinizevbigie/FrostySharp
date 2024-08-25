
<h1 align="center">Frosty</h1>
<h3 align="center">An Email Sales System for Creative Agencies.</h3>

---

<p align="center" width="100%">
    <img width="33%" src="Frosty.png">
</p>

---

### What Is Frosty

Frosty is a Email Outreach tool I build for my former marketing Agency, S2 Digital. 

We had a big problem. The available SaaS on the market for email outreach didn't fit our needs. They were either loaded with lot's of features we didn't need (and they charged a premium for it). Or, they removed features we did need. As a result, our sales process continued to change. And thus, remained inconsistent.

For that reason, I built Frosty for consistency and ownership of the outreach process.

The second reason I build Frosty was because of email deliverability. Deliverability is never taken into account with SaaS (in 2018). 

There is a fine line between `email outreach` and `spam email`. Our agencies' reputation and our email sending accounts must remain credible. Thus, we took the view that "how" we send emails was important.

We built these deliverability ideas into Frosty.

---

### What is this project? Is it complete?

Yes, and no.

The production version of Frosty was went live in March 2018. And we used to to generate over $500,000 in revenue between 2018 - 2022. 

Sadly, the impact of Covid destroyed the business. Because most of our clients were local businesses. Most of which, closed their doors during Covid. The production version is still in use (maybe?) by it's new owners.

This project is a recreation of the `Domain Layer` from the original 2018 version. Which means all the improvements made after 2018 have been ignored. Because they are business secrets.

---

### So, Why did I build this?

It's a portfolio project that shows how I:

- [Work on projects](https://github.com/users/Kevinizevbigie/projects/3)
- How I create domain entities
- How I create domain Value Objects
- How I make software decisions
- How I write unit tests
- And how I refactor unit tests
- How I plan ahead for Application services



---

### Application Layer Plan

This project only contains the Domain Layer. If read, you may infer some of the application services. It's worth making it explicit how I intend to deal with it.

The application layer will use `MediatR` to create the CQRS pattern. There will be `queries`, `query handlers`, `commands`, and `command handlers`. Each query and command will live in a use-case specific folder. 

For example `GetRecordQuery` is a query that will be in the `Frosty.Application.Record.GetRecord` namespace and folder. Equally, the command `AddToSalesPipelineCommand` will be in `Frosty.Application.Record.AddToPipeline` namespace and folder.

These folders will have the query/command and their respective handler.

Having a long list of use-case based folders makes it very easy to find, edit and extend specific features.
