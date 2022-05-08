
CREATE TABLE public."Person"
(
    "Row" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Id" character(50) NOT NULL,
    "Name" character(50) NOT NULL,
    "Age" integer NOT NULL,
    "Birthday" date NOT NULL,
    "Remark" character(100),
    CONSTRAINT "PK_Person" PRIMARY KEY ("Row"),
    CONSTRAINT "UNIQUE_Person_Id" UNIQUE ("Id")
);

ALTER TABLE IF EXISTS public."Person"
    OWNER to postgres;

CREATE TABLE public."Address"
(
    "Row" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Id" character(50) NOT NULL,
    "Text" character(100) NOT NULL,
    CONSTRAINT "PK_Address" PRIMARY KEY ("Row"),
    CONSTRAINT "FK_Address_Id" FOREIGN KEY ("Id")
        REFERENCES public."Person" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE IF EXISTS public."Address"
    OWNER to postgres;
	
insert into public."Person"
("Id","Name","Age","Birthday","Remark")
VALUES
('A123456789','AAA',18,CURRENT_DATE,''),
('B123456789','BBB',28,CURRENT_DATE,'')

INSERT INTO public."Address"
("Id","Text")
VALUES
('A123456789','A1'),
('A123456789','A2'),
('B123456789','B1'),
('B123456789','B2')
