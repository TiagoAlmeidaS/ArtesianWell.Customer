CREATE TABLE IF NOT EXISTS "Customers" (
                                           "Id" text NOT NULL,
                                           "Name" text NOT NULL,
                                           "Email" text NOT NULL,
                                           "Phone" text NOT NULL,
                                           "ProfileTypeId" int NOT NULL,
                                           "Document" text UNIQUE NOT NULL,
                                           "CreatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    "UpdatedAt" timestamp with time zone NOT NULL DEFAULT now(),
    CONSTRAINT "PK_Customers" PRIMARY KEY ("Id")
);

