Feature: Redirect to correct popup page based on the term id, lang, dictionary and audience


    ##################  HTML  #####################
    Scenario: HTML is displayed when dictionary is not set, for patient audience and english lang
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=659892&version=patient&language=English&dictionary=NotSet"
        Then the HTML is displayed
        And the title tag should be "Definition of M"
        And "robots" metatag exists is the data for the page with content "noindex, nofollow"
        And the page title is "M"
        And term description is "In chemistry, M is the amount of a substance that has 6.023 x 10(23) atoms or molecules of that substance. Also called mole (chemical)."

    Scenario: HTML is displayed when dictionary is not set, for health professional audience and english lang
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=270731&version=healthprofessional&language=English&dictionary=NotSet"
        Then the HTML is displayed
        And the title tag should be "Definition of 3-dimensional conformal radiation therapy"
        And "robots" metatag exists is the data for the page with content "noindex, nofollow"
        And the page title is "3-dimensional conformal radiation therapy"
        And term description is "3-dimensional conformal radiation therapy involves the use of computed tomography (CT) imaging in the planning of radiation therapy. The CT scan provides not only 3-dimensional imaging of the target and surrounding normal tissues, but also information about tissue density and tissue depth from the skin to the target. These parameters are critical in calculating the dose distribution. In addition to CT imaging, supplemental imaging modalities, such as magnetic resonance imaging or positron emission tomography, can be used to improve target delineation. With 3-dimensional conformal radiation therapy, conformal beams are used to shape the dose delivered to the target, and wedges or compensators can be used to optimize the dose distribution. Conformal beams are shaped either with a high-density material (e.g., Cerrobend) that allows beam contouring or with multi-leaf collimators, which are an array of high-density leaves (usually tungsten) situated in the head of the linear accelerator (LINAC) whose position is controlled via independent stepping motors that allow beam shaping. Wedges are high-density devices that are placed on the head of the LINAC to act as a tissue compensator and/or beam modifier. The effect of a wedge can be created by a moving jaw at the head of the LINAC. With 3-dimensional conformal radiation therapy, variable field weighting and/or use of different energies (higher energies are more penetrating) are additional tools that enable optimization of the dose distribution.  Also called 3-dimensional radiation therapy and 3D-CRT."

    Scenario: HTML is displayed when all params are set, but the dictionary parameter does not match what is set on term (English Genetics for patients)
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=CDR0000045722&version=Patient&language=English&dictionary=Genetics"
        Then the HTML is displayed
        And the title tag should be "Definition of immune response"
        And "robots" metatag exists is the data for the page with content "noindex, nofollow"
        And the page title is "immune response"
        And term description is "The way the body defends itself against substances it sees as harmful or foreign. In an immune response, the immune system recognizes the antigens (usually proteins) on the surface of substances or microorganisms, such as bacteria or viruses, and attacks and destroys, or tries to destroy, them. Cancer cells also have antigens on their surface. Sometimes, the immune system sees these antigens as foreign and mounts an immune response against them. This helps the body fight cancer."

    Scenario: HTML is displayed when all params are set, but the parameter does not match what is set on term (Spanish Genetics Health Professionals)
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=798928&version=healthprofessional&language=Spanish&dictionary=Cancer.gov"
        Then the HTML is displayed
        And the title tag should be "Definition of DC"
        And "robots" metatag exists is the data for the page with content "noindex, nofollow"
        And the page title is "DC"
        And term description is "Trastorno hereditario poco frecuente que afecta muchas partes del cuerpo, en especial, las uñas, la piel y la boca. Se caracteriza por uñas de forma anormal que crecen muy poco; cambios en el color de la piel, en especial, en el cuello y el tórax; así como manchas blancas dentro de la boca. Otros problemas son caída del pelo, canas prematuras, problemas en los ojos y los dientes, osteoporosis, problemas articulares, enfermedad del hígado y estrechamiento de la uretra  (tubo por el que sale la orina de la vejiga) en los hombres. Las personas con DC a veces presentan afecciones graves, como insuficiencia de la  médula ósea, síndrome mielodisplásico fibrosis pulmonar y determinados tipos de cáncer, en especial, leucemias y cánceres de cabeza y cuello, boca, ano y órganos genitales. Es posible que la causa de la  DC sean mutaciones (cambios) en determinados genes que afectan el largo de los telómeros (extremos de los cromosomas). También se llama disqueratosis congénita."

    Scenario: HTML is displayed when all params are set, but the parameter does not match what is set on term(Spanish Genetics patient)
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=798928&version=patient&language=Spanish&dictionary=Cancer.gov"
        Then the HTML is displayed
        And the title tag should be "Definition of DC"
        And "robots" metatag exists is the data for the page with content "noindex, nofollow"
        And the page title is "DC"
        And term description is "Trastorno hereditario poco frecuente que afecta muchas partes del cuerpo, en especial, las uñas, la piel y la boca. Se caracteriza por uñas de forma anormal que crecen muy poco; cambios en el color de la piel, en especial, en el cuello y el tórax; así como manchas blancas dentro de la boca. Otros problemas son caída del pelo, canas prematuras, problemas en los ojos y los dientes, osteoporosis, problemas articulares, enfermedad del hígado y estrechamiento de la uretra  (tubo por el que sale la orina de la vejiga) en los hombres. Las personas con DC a veces presentan afecciones graves, como insuficiencia de la  médula ósea, síndrome mielodisplásico fibrosis pulmonar y determinados tipos de cáncer, en especial, leucemias y cánceres de cabeza y cuello, boca, ano y órganos genitales. Es posible que la causa de la  DC sean mutaciones (cambios) en determinados genes que afectan el largo de los telómeros (extremos de los cromosomas). También se llama disqueratosis congénita."


    ##################  Redirects  #####################
    Scenario: Redirected to the definition page when all parameters are matched those set on term, English cancer terms for patients
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=CDR0000045722&version=Patient&language=English&dictionary=Cancer.gov"
        Then user is redirected to "/publications/dictionaries/cancer-terms/def/immune-response" on cancer.gov

    Scenario: Redirected to the definition page when all parameters are matched those set on term, Spanish cancer terms for patients
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=335061&version=patient&language=Spanish&dictionary=Cancer.gov"
        Then user is redirected to "/espanol/publicaciones/diccionario/def/ablacion" on cancer.gov

    Scenario: Redirected to the definition page when all parameters are matched those set on term, English Genetics for HealthProfessionals
        Given user is navigating to "/Common/PopUps/popDefinition.aspx?id=781843&version=healthprofessional&language=English&dictionary=Genetics"
        Then user is redirected to "/publications/dictionaries/genetics-dictionary/def/acrochordon" on cancer.gov


    ##################  Page Not Found  #####################

    Scenario: Redirected to the Page not Found when invalid CDR ID provided
        Given user is navigating to error page "/Common/PopUps/popDefinition.aspx?id=12345&version=Patient&language=English&dictionary=Cancer.gov"
        Then the error page title is "Page Not Found"
