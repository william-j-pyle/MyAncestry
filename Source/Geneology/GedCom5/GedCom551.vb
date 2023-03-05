Public Class GedCom551
    'n << FAM_RECORD >> {1: 1} p.24
    'n << INDIVIDUAL_RECORD >> {1: 1} p.25
    'n << MULTIMEDIA_RECORD >> {1: 1} p.26
    'n << NOTE_RECORD >> {1: 1} p.27
    'n << REPOSITORY_RECORD >> {1: 1} p.27
    'n << SOURCE_RECORD >> {1: 1} p.27
    'n << SUBMITTER_RECORD >> {1: 1} p.28
End Class

Public Class GedCom_Line
    'level +delim + [optional_xref_ID] + tag + [optional_line_value] + terminator
End Class

'<<double_angle bracket>>
'Indicates that a subordinate GEDCOM Structure pattern Of either a record, Structure, Or
'substructure Is to be substituted in place of the line containing the enclosing double angle
'brackets.The substitute structure pattern Is found subordinate to the
'LINEAGE_LINKED_GEDCOM beginning On page 24 For record pattern definition Or In
'alphabetical order under the "Substructures of the Lineage-Linked Form" section, beginning On
'page 31.

'<Single_angle bracket>
'20
'Indicates the name Of the appropriate value For this GEDCOM line— <Primitive>. The specific
'definition of this value Is found in alphabetical order in "Primitive Elements of the Lineage-Linked
'Form," beginning on page 41.

'{braces}
'Indicates the minimum To maximum occurrences allowed For this Structure Or
'line— {Minimum:Maximum}. Note that minimum And maximum occurrence limits are defined
'relative to the enclosing superior line. This means that a required line (minimum = 1) Is Not
'required If the optional enclosing superior line Is Not present. Similarly, a line occurring only
'once(maximum = 1) may occur multiple times As Long As Each occurs only once under its own
'multiple-occurring superior line.

'[Square brackets]
'Indicates a choice Of one Or more options— [Choice Of].

'| vertical bar |
'Separates the multiple choices, For example [Choice 1 | Choice 2].

'n level number
'A level number which assumes the level number Of the line which referenced the substructure
'name.

'+1, +2 ...
'A +1 level number Is 1 greater than the level number assumed by the superior n level. A +2 level
'number Is 2 greater, And so forth.

'0xHH
'Indicates an allowable hexadecimal character value where HH Is that value, For example, 0x20
'(decimal 32) indicates the space character.


Public Class GED_HEADER
    'n HEAD {1:1}
    '+1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
    '+2 VERS <VERSION_NUMBER> {0:1} p.64
    '+2 NAME <NAME_OF_PRODUCT> {0:1} p.54
    '+2 CORP <NAME_OF_BUSINESS> {0:1} p.54
    '+3 <<ADDRESS_STRUCTURE>> {0:1} p.31
    '+2 DATA <NAME_OF_SOURCE_DATA> {0:1} p.54
    '+3 DATE <PUBLICATION_DATE> {0:1) p.59
    '+3 COPR <COPYRIGHT_SOURCE_DATA> {0:1) p.44
    '+4 [CONT|CONC]<COPYRIGHT_SOURCE_DATA> {0:M} p.44
    '+1 DEST <RECEIVING_SYSTEM_NAME> {0:1* p.59
    '+1 DATE <TRANSMISSION_DATE> {0:1} p.63
    '+2 TIME <TIME_VALUE> {0:1} p.63
    '+1 SUBM @<XREF:SUBM>@ {1:1} p.28
    '+1 SUBN @<XREF:SUBN>@ {0:1} p.28
    '+1 FILE <FILE_NAME> {0:1} p.50
    '+1 COPR <COPYRIGHT_GEDCOM_FILE> {0:1} p.44
    '+1 GEDC {1:1}
    '+2 VERS <VERSION_NUMBER> {1:1} p.64
    '+2 FORM <GEDCOM_FORM> {1:1} p.50
    '+1 CHAR <CHARACTER_SET> {1:1} p.44
    '+2 VERS <VERSION_NUMBER> {0:1} p.64
    '+1 LANG <LANGUAGE_OF_TEXT> {0:1} p.51
    '+1 PLAC {0:1}
    '+2 FORM <PLACE_HIERARCHY> {1:1} p.58
    '+1 NOTE <GEDCOM_CONTENT_DESCRIPTION> {0:1} p.50
    '24
    '+2 [CONC|CONT] <GEDCOM_CONTENT_DESCRIPTION> {0:M}
End Class

'The FAMily record Is used To record marriages, common law marriages, And family unions caused by
'two people becoming the parents Of a child. There can be no more than one HUSB/father And one
'WIFE/ mother listed In Each FAM_RECORD. If, for example, a man participated in more than one
'family union, then he would appear In more than one FAM_RECORD. The family record Structure
'assumes that the HUSB/father Is male And WIFE/mother Is female.
'The preferred order Of the CHILdren pointers within a FAMily Structure Is chronological by birth.
Public Class GED_FAM_RECORD
    'n @<XREF:FAM>@ FAM {1:1}
    '+1 RESN <RESTRICTION_NOTICE> {0:1) p.60
    '+1 <<FAMILY_EVENT_STRUCTURE>> {0:M} p.32
    '+1 HUSB @<XREF:INDI>@ {0:1} p.25
    '+1 WIFE @<XREF:INDI>@ {0:1} p.25
    '+1 CHIL @<XREF:INDI>@ {0:M} p.25
    '+1 NCHI <COUNT_OF_CHILDREN> {0:1} p.44
    '+1 SUBM @<XREF:SUBM>@ {0:M} p.28
    '+1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
    '+1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
    '25
    '+2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<CHANGE_DATE>> {0:1} p.31
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 <<SOURCE_CITATION>> {0:M} p.39
    '+1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
End Class

'The individual record Is a compilation Of facts, known Or discovered, about an individual. Sometimes
'these facts are from different sources. This form allows documentation Of the source where Each Of
'26 the facts were discovered.
'The normal lineage links are shown through the use Of pointers from the individual To a family
'through either the FAMC tag Or the FAMS tag. The FAMC tag provides a pointer To a family where
'this person Is a child. The FAMS tag provides a pointer To a family where this person Is a spouse Or
'parent.The << CHILD_TO_FAMILY_LINK >> (see page 31) structure contains a FAMC pointer
'which Is required to show any child to parent linkage for pedigree navigation. The
'<<CHILD_TO_FAMILY_LINK>> structure also indicates whether the pedigree link represents a
'birth lineage, an adoption lineage, Or a sealing lineage.
'Linkage between a child And the family they belonged To at the time Of an Event can also be shown
'by a FAMC pointer subordinate To the appropriate Event. For example, a FAMC pointer subordinate
'to an adoption event indicates a relationship to family by adoption. Biological parents can be shown
'by a FAMC pointer subordinate To the birth Event(Optional).
'Other associations Or relationships are represented by the ASSOciation tag. The person's relation
'Or association Is the person being pointed to. The association Or relationship Is stated by the value
'On the subordinate RELA line. For example
'0 @I1@ INDI
'1 NAME Fred/Jones/
'1 ASSO @I2@
'2 RELA Godfather
Public Class GED_INDIVIDUAL_RECORD
    'n @XREF:INDI@ INDI {1:1}
    '+1 RESN <RESTRICTION_NOTICE> {0:1} p.60
    '+1 <<PERSONAL_NAME_STRUCTURE>> {0:M} p.38
    '+1 SEX <SEX_VALUE> {0:1} p.61
    '+1 <<INDIVIDUAL_EVENT_STRUCTURE>> {0:M} p.34
    '+1 <<INDIVIDUAL_ATTRIBUTE_STRUCTURE>> {0:M} p.33
    '+1 <<LDS_INDIVIDUAL_ORDINANCE>> {0:M} p.35, 36
    '+1 <<CHILD_TO_FAMILY_LINK>> {0:M} p.31
    '+1 <<SPOUSE_TO_FAMILY_LINK>> {0:M} p.40
    '+1 SUBM @<XREF:SUBM>@ {0:M} p.28
    '+1 <<ASSOCIATION_STRUCTURE>> {0:M} p.31
    '+1 ALIA @<XREF:INDI>@ {0:M} p.25
    '+1 ANCI @<XREF:SUBM>@ {0:M} p.28
    '+1 DESI @<XREF:SUBM>@ {0:M} p.28
    '+1 RFN <PERMANENT_RECORD_FILE_NUMBER> {0:1} p.57
    '+1 AFN <ANCESTRAL_FILE_NUMBER> {0:1} p.42
    '+1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
    '+2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<CHANGE_DATE>> {0:1} p.31
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 <<SOURCE_CITATION>> {0:M} p.39
    '+1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
End Class


'A reference To a multimedia
'file was added To the record Structure. The file reference occurs one To many times so that multiple files
'can be grouped together, Each pertaining To the same context. For example, If you wanted To associate a
'sound clip And a photo, you would reference Each multimedia file And indicate the format Using the
'FORM tag subordinate To Each file reference.
Public Class GED_MULTIMEDIA_RECORD
    'n @XREF:OBJE@ OBJE {1:1}
    '+1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
    '+2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
    '+3 TYPE <SOURCE_MEDIA_TYPE> {0:1} p.62
    '+2 TITL <DESCRIPTIVE_TITLE> {0:1} p.48
    '+1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
    '+2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 <<SOURCE_CITATION>> {0:M} p.39
    '+1 <<CHANGE_DATE>> {0:1} p.31
End Class

Public Class GED_NOTE_RECORD
    'n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT> {1:1} p.63
    '+1 [CONC|CONT] <SUBMITTER_TEXT> {0:M}
    '+1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
    '+2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<SOURCE_CITATION>> {0:M} p.39
    '+1 <<CHANGE_DATE>> {0:1} p.31
End Class

Public Class GED_REPOSITORY_RECORD
    'n @<XREF:REPO>@ REPO {1:1}
    '+1 NAME <NAME_OF_REPOSITORY> {1:1} p.54
    '+1 <<ADDRESS_STRUCTURE>> {0:1} p.31
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
    '+2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<CHANGE_DATE>> {0:1} p.31
End Class

'Source records are used To provide a bibliographic description Of the source cited. (See the
'<<SOURCE_CITATION>> structure, page 39, which contains the pointer to this source record.)
Public Class GED_SOURCE_RECORD
    'n @<XREF:SOUR>@ SOUR {1:1}
    '+1 DATA {0:1}
    '+2 EVEN <EVENTS_RECORDED> {0:M} p.50
    '+3 DATE <DATE_PERIOD> {0:1} p.46
    '+3 PLAC <SOURCE_JURISDICTION_PLACE> {0:1} p.62
    '+2 AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
    '+2 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 AUTH <SOURCE_ORIGINATOR> {0:1} p.62
    '+2 [CONC|CONT] <SOURCE_ORIGINATOR> {0:M} p.62
    '+1 TITL <SOURCE_DESCRIPTIVE_TITLE> {0:1} p.62
    '+2 [CONC|CONT] <SOURCE_DESCRIPTIVE_TITLE> {0:M} p.62
    '+1 ABBR <SOURCE_FILED_BY_ENTRY> {0:1} p.62
    '+1 PUBL <SOURCE_PUBLICATION_FACTS> {0:1} p.62
    '+2 [CONC|CONT] <SOURCE_PUBLICATION_FACTS> {0:M} p.62
    '+1 TEXT <TEXT_FROM_SOURCE> {0:1} p.63
    '+2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M} p.63
    '+1 <<SOURCE_REPOSITORY_CITATION>> {0:M} p.40
    '+1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
    '+2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<CHANGE_DATE>> {0:1} p.31
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
End Class

'The sending system uses a submission record To send instructions And information To the receiving
'system.TempleReady processes submission records to determine which temple the cleared records
'should be directed To. The submission record Is also used For communication between Ancestral File
'download requests And TempleReady. Each GEDCOM transmission file should have only one
'submission record. Multiple submissions are handled by creating separate GEDCOM transmission
'files.
Public Class GED_SUBMISSION_RECORD
    'n @XREF:SUBN@ SUBN {1:1}
    '+1 SUBM @XREF:SUBM@ {0: 1} p.28
    '+1 FAMF <NAME_OF_FAMILY_FILE> {0:1} p.54
    '+1 TEMP <TEMPLE_CODE> {0:1} p.63
    '+1 ANCE <GENERATIONS_OF_ANCESTORS> {0:1} p.50
    '+1 DESC <GENERATIONS_OF_DESCENDANTS> {0:1} p.50
    '+1 ORDI <ORDINANCE_PROCESS_FLAG> {0:1} p.57
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 <<CHANGE_DATE>> {0:1} p.31
End Class

'The submitter record identifies an individual Or organization that contributed information contained
'in the GEDCOM transmission. All records in the transmission are assumed to be submitted by the
'SUBMITTER referenced In the HEADer, unless a SUBMitter reference inside a specific record
'points at a different SUBMITTER record.
Public Class GED_SUBMITTER_RECORD
    'n @<XREF:SUBM>@ SUBM {1:1}
    '+1 NAME <SUBMITTER_NAME> {1:1} p.63
    '+1 <<ADDRESS_STRUCTURE>> {0:1}* p.31
    '+1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
    '+1 LANG <LANGUAGE_PREFERENCE> {0:3} p.51
    '+1 RFN <SUBMITTER_REGISTERED_RFN> {0:1} p.63
    '+1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 <<CHANGE_DATE>> {0:1} p.31
End Class

'The address Structure should be formed As it would appear On a mailing label Using the ADDR And
'the CONT lines To form the address Structure. The ADDR And CONT lines are required For any
'address.The additional subordinate address tags such as STAE And CTRY are provided to be used
'by systems that have structured their addresses For indexing And sorting. For backward compatibility
'these lines are Not To be used In lieu Of the required ADDR.And CONT line Structure.
Public Class GED_ADDRESS_STRUCTURE
    'n ADDR <ADDRESS_LINE> {1:1} p.41
    '+1 CONT <ADDRESS_LINE> {0:3} p.41
    '+1 ADR1 <ADDRESS_LINE1> {0:1} p.41
    '+1 ADR2 <ADDRESS_LINE2> {0:1} p.41
    '+1 ADR3 <ADDRESS_LINE3> {0:1} p.41
    '+1 CITY <ADDRESS_CITY> {0:1} p.41
    '+1 STAE <ADDRESS_STATE> {0:1} p.42
    '+1 POST <ADDRESS_POSTAL_CODE> {0:1} p.41
    '+1 CTRY <ADDRESS_COUNTRY> {0:1} p.41
    'n PHON <PHONE_NUMBER> {0:3} p.57
    'n EMAIL <ADDRESS_EMAIL> {0:3} p.41
    'n FAX <ADDRESS_FAX> {0:3} p.41
    'n WWW <ADDRESS_WEB_PAGE> {0:3} p.42
End Class

'The association pointer only associates INDIvidual records To INDIvidual records.
Public Class GED_ASSOCIATION_STRUCTURE
    'n ASSO @<XREF:INDI>@ {1:1} p.25
    '+1 RELA <RELATION_IS_DESCRIPTOR> {1:1} p.60
    '+1 <<SOURCE_CITATION>> {0:M} p.39
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
End Class

'The change Date Is intended To only record the last change To a record. Some systems may want To
'manage the change process With more detail, but it Is sufficient For GEDCOM purposes To indicate
'the last time that a record was modified.
Public Class GED_CHANGE_DATE
    'n CHAN {1:1}
    '+1 DATE <CHANGE_DATE> {1:1} p.44
    '+2 TIME <TIME_VALUE> {0:1} p.63
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
End Class

Public Class GED_CHILD_TO_FAMILY_LINK
    'n FAMC @<XREF:FAM>@ {1:1} p.24
    '32
    '+1 PEDI <PEDIGREE_LINKAGE_TYPE> {0:1} p.57
    '+1 STAT <CHILD_LINKAGE_STATUS> {0:1} p.44
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
End Class

Public Class GED_EVENT_DETAIL
    'n TYPE <EVENT_OR_FACT_CLASSIFICATION> {0:1} p.49
    'n Date < DATE_VALUE > {0: 1} p.47, 46
    'n << PLACE_STRUCTURE >> {0: 1} p.38
    'n << ADDRESS_STRUCTURE >> {0: 1} p.31
    'n AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
    'n RELI <RELIGIOUS_AFFILIATION> {0:1} p.60
    'n CAUS <CAUSE_OF_EVENT> {0:1} p.43
    'n RESN <RESTRICTION_NOTICE> {0:1} p.60
    'n << NOTE_STRUCTURE >> {0: M} p.37
    'n << SOURCE_CITATION >> {0: M} p.39
    'n << MULTIMEDIA_LINK >> {0: M} p.37, 26
End Class

Public Class GED_FAMILY_EVENT_DETAIL
    'n HUSB {0:1}
    '+1 AGE <AGE_AT_EVENT> {1:1} p.42
    'n WIFE {0:1}
    '+1 AGE <AGE_AT_EVENT> {1:1} p.42
    'n << EVENT_DETAIL >> {0: 1} p.32
End Class

Public Class GED_FAMILY_EVENT_STRUCTURE
    '[
    'n [ ANUL | CENS | DIV | DIVF ] {1:1}
    '+1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
    '|
    'n [ ENGA | MARB | MARC ] {1:1}
    '+1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
    '|
    'n MARR [Y|<NULL>] {1:1}
    '+1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
    '|
    'n [ MARL | MARS ] {1:1}
    '+1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
    '|
    'n RESI
    '+1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
    '|
    'n EVEN [<EVENT_DESCRIPTOR> | <NULL>] {1:1} p.48
    '+1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
    '33
    ']
End Class

'* Note: The usage Of IDNO Or the FACT tag require that a subordinate TYPE tag be used To define
'what kind Of identification number Or fact classification Is being defined. The TYPE tag can be used
'With each of the above tags used in this structure.
Public Class GED_INDIVIDUAL_ATTRIBUTE_STRUCTURE
    '[
    'n CAST <CASTE_NAME> {1:1} p.43
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n DSCR <PHYSICAL_DESCRIPTION> {1:1} p.58
    '+1 [CONC | CONT ] <PHYSICAL_DESCRIPTION> {0:M} p.58
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n EDUC <SCHOLASTIC_ACHIEVEMENT> {1:1} p.61
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n IDNO <NATIONAL_ID_NUMBER> {1:1} p.56
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n NATI <NATIONAL_OR_TRIBAL_ORIGIN> {1:1} p.56
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n NCHI <COUNT_OF_CHILDREN> {1:1} p.44
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n NMR <COUNT_OF_MARRIAGES> {1:1} p.44
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n OCCU <OCCUPATION> {1:1} p.57
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n PROP <POSSESSIONS> {1:1} p.59
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n RELI <RELIGIOUS_AFFILIATION> {1:1} p.60
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n RESI /* Resides at */ {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n SSN <SOCIAL_SECURITY_NUMBER> {1:1} p.61
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n TITL <NOBILITY_TYPE_TITLE> {1:1} p.57
    '34
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n FACT <ATTRIBUTE_DESCRIPTOR> {1:1} p.43
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    ']
End Class

Public Class GED_INDIVIDUAL_EVENT_DETAIL
    'n << EVENT_DETAIL >> {1: 1} p.32
    'n AGE <AGE_AT_EVENT> {0:1} p.42
End Class

'As a general rule, events are things that happen on a specific date. Use the date form 'BET date
'And date' to indicate that an event took place at some time between two dates. Resist the
'temptation to use a 'FROM date TO date’ form in an event structure. If the subject of your
'recording occurred over a period Of time, Then it Is probably Not an Event, but rather an attribute Or
'fact.
'The EVEN tag in this structure Is for recording general events that are Not shown in the above
'<<INDIVIDUAL_EVENT_STRUCTURE>>. The event indicated by this general EVEN tag Is
'defined by the value Of the subordinate TYPE tag. For example, a person that signed a lease For land
'dated October 2, 1837 And a lease For equipment dated November 4, 1837 would be written In
'GEDCOM as
'1 EVEN 
'2 TYPE Land Lease
'2 DATE 2 OCT 1837
'1 EVEN 
'2 TYPE Equipment Lease
'2 DATE 4 NOV 1837
'The TYPE tag can be optionally used To modify the basic understanding Of its superior Event Or
'attribute.For example
'1 GRAD
'2 TYPE College 
'The occurrence Of an Event Is asserted by the presence Of either a Date tag And value Or a PLACe
'tag And value in the event structure. When neither the date value nor the place value are known then
'a Y(es) value On the parent Event tag line Is required To assert that the Event happened. For example
'each of the following GEDCOM structures assert that a death happened
'1 DEAT Y 
'1 DEAT
'2 DATE 2 OCT 1937
'1 DEAT
'2 PLAC Cove, Cache, Utah
'36
'Using this convention, As opposed To the just the presence Of the tag, protects GEDCOM processors
'which removes(prunes) lines which have neither a value nor any subordinate line. It also allows a
'note Or source to be attached to an event context without implying that the event occurred.
'It Is Not proper GEDCOM form to use a N(o) value with an event tag to infer that it did Not happen. 
'A convention To handle events which never happened may be defined In the future. 
Public Class GED_INDIVIDUAL_EVENT_STRUCTURE
    '[
    'n [ BIRT | CHR ] [Y|<NULL>] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '+1 FAMC @<XREF:FAM>@ {0:1} p.24
    '|
    'n DEAT [Y|<NULL>] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n [ BURI | CREM ] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n ADOP {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '+1 FAMC @<XREF:FAM>@ {0:1} p.24
    '+2 ADOP <ADOPTED_BY_WHICH_PARENT> {0:1} p.42
    '|
    'n [ BAPM | BARM | BASM | BLES ] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n [ CHRA | CONF | FCOM | ORDN ] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n [ NATU | EMIG | IMMI ] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n [ CENS | PROB | WILL] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    '35
    'n [ GRAD | RETI ] {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    '|
    'n EVEN {1:1}
    '+1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    ']
End Class

Public Class GED_MULTIMEDIA_LINK
    'n OBJE @<XREF:OBJE>@ {1:1} p.26
    '|
    'n OBJE
    '+1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
    '+2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
    '+3 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62
    '+1 TITL <DESCRIPTIVE_TITLE> {0:1} p.48
    'Note: some systems may have output the following 5.5 Structure. The New context above was
    'introduced in order to allow a grouping of related multimedia files to a particular context.
    'n OBJE
    '+1 FILE
    '+1 FORM <MULTIMEDIA_FORMAT>
    '+2 MEDI <SOURCE_MEDIA_TYPE>
End Class

'Note: There are special considerations required When Using the CONC tag. The usage Is To provide a
'note String that can be concatenated together so that the display program can do its own word
'wrapping according To its display window size. The requirement For usage Is To either break the text
'line in the middle of a word, Or If at the end of a word, to add a space to the first of the next CONC
'line.Otherwise most operating systems will strip off the trailing space And the space Is lost in the
'reconstitution of the note. 
Public Class GED_NOTE_STRUCTURE
    '[
    'n NOTE @<XREF:NOTE>@ {1:1} p.27
    '|
    'n NOTE [<SUBMITTER_TEXT> | <NULL>] {1:1} p.63
    '+1 [CONC|CONT] <SUBMITTER_TEXT> {0:M}
    ']
End Class

Public Class GED_PERSONAL_NAME_PIECES
    'n NPFX <NAME_PIECE_PREFIX> {0:1} p.55
    'n GIVN <NAME_PIECE_GIVEN> {0:1} p.55
    'n NICK <NAME_PIECE_NICKNAME> {0:1} p.55
    'n SPFX <NAME_PIECE_SURNAME_PREFIX {0:1} p.56
    '38
    'n SURN <NAME_PIECE_SURNAME> {0:1} p.55
    'n NSFX <NAME_PIECE_SUFFIX> {0:1} p.55
    'n << NOTE_STRUCTURE >> {0: M} p.37
    'n << SOURCE_CITATION >> {0: M} p.39
End Class

'The name value Is formed In the manner the name Is normally spoken, With the given name And family
'name(surname) separated by slashes (/). (See <NAME_PERSONAL>, page 54.) Based On the
'dynamic nature Or unknown compositions Of naming conventions, it Is difficult To provide more
'detailed name piece Structure To handle every Case. The NPFX, GIVN, NICK, SPFX, SURN, And
'NSFX tags are provided optionally For systems that cannot operate effectively With less structured
'information.For current future compatibility, all systems must construct their names based on the
'<NAME_PERSONAL> Structure. Those using the optional name pieces should assume that few
'systems will process them, And most will Not provide the name pieces.
'A <NAME_TYPE> Is used to specify the particular variation that this name Is. For example; if the
'name type Is subordinate To the <NAME_PERSONAL> it could indicate that this name Is a name
'taken at immigration Or that it could be an 'also known as’ name (see page 56.)
'Future GEDCOM releases (6.0 Or later) will likely apply a very different strategy To resolve this
'problem, possibly using a sophisticated parser And a name-knowledge database.
Public Class GED_PERSONAL_NAME_STRUCTURE
    'n NAME <NAME_PERSONAL> {1:1} p.54
    '+1 TYPE <NAME_TYPE> {0:1} p.56
    '+1 <<PERSONAL_NAME_PIECES>> {0:1} p.37
    '+1 FONE <NAME_PHONETIC_VARIATION> {0:M} p.55
    '+2 TYPE <PHONETIC_TYPE> {1:1} p.57
    '+2 <<PERSONAL_NAME_PIECES>> {0:1} p.37
    '+1 ROMN <NAME_ROMANIZED_VARIATION> {0:M} p.56
    '+2 TYPE <ROMANIZED_TYPE> {1:1} p.61
    '+2 <<PERSONAL_NAME_PIECES>> {0:1} p.37
End Class

Public Class GED_PLACE_STRUCTURE
    'n PLAC <PLACE_NAME> {1:1} p.58
    '+1 FORM <PLACE_HIERARCHY> {0:1} p.58
    '39
    '+1 FONE <PLACE_PHONETIC_VARIATION> {0:M} p.59
    '+2 TYPE <PHONETIC_TYPE> {1:1} p.57
    '+1 ROMN <PLACE_ROMANIZED_VARIATION> {0:M} p.59
    '+2 TYPE <ROMANIZED_TYPE> {1:1} p.61
    '+1 MAP {0:1}
    '+2 LATI <PLACE_LATITUDE> {1:1} p.58
    '+2 LONG <PLACE_LONGITUDE> {1:1} p.58
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
End Class

'The data provided In the <<SOURCE_CITATION>> Structure Is source-related information specific
'to the data being cited. (See GEDCOM examples starting on page 74.) Systems that do Not use a
'(SOURCE_RECORD) must use the non-preferred second SOURce citation structure option. When
'systems that support the zero level source record format encounters a source citation that does Not
'contain pointers To source records, Then that system needs To create a SOURCE_RECORD format
'And store the source description information found in the non-structured source citation in the title
'area for the New source record.
'The information intended To be placed In the citation Structure includes
'!The pointer To the SOURCE_RECORD, which contains a more general description of the source
'used for the fact being cited.
'!Information, such As a page number, to help the user find the cited data within the referenced
'source. This Is stored in the “.SOUR.PAGE” tag context.
'!Actual text from the source that was used In making assertions, for example a date phrase as
'actually recorded In the source, Or significant notes written by the recorder, Or an applicable
'sentence from a letter. This Is stored In the “.SOUR.DATA.TEXT” tag context.
'!Data that allows an assessment of the relative value of one source over another for making the
'recorded assertions(primary Or secondary source, etc.). Data needed For this assessment Is data
'that would help determine how much time from the Date Of the asserted fact And When the source
'was actually recorded, what type Of Event was cited, And what type Of role did this person have In
'the cited source.
'- Date when the entry was recorded in source document Is stored in the 
'".SOUR.DATA.DATE" tag context.
'- The type of event that initiated the recording Is stored in the “SOUR.EVEN” tag context. The
'value used Is the Event code taken from the table Of choices shown In the
'EVENT_TYPE_CITED_FROM primitive On page 49
'- The role of this person in the event Is stored in the ".SOUR.EVEN.ROLE" context.
Public Class GED_SOURCE_CITATION
    '[ /* pointer to source record (preferred)*/
    'n SOUR @<XREF:SOUR>@ {1:1} p.27
    '+1 PAGE <WHERE_WITHIN_SOURCE> {0:1} p.64
    '+1 EVEN <EVENT_TYPE_CITED_FROM> {0:1} p.49
    '+2 ROLE <ROLE_IN_EVENT> {0:1} p.61
    '+1 DATA {0:1}
    '+2 DATE <ENTRY_RECORDING_DATE> {0:1} p.48
    '+2 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
    '+3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
    '+1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 QUAY <CERTAINTY_ASSESSMENT> {0:1} p.43
    '| /* Systems Not using source records */
    'n SOUR <SOURCE_DESCRIPTION> {1:1} p.61
    '+1 [CONC|CONT] <SOURCE_DESCRIPTION> {0:M}
    '+1 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
    '+2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
    '+1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 QUAY <CERTAINTY_ASSESSMENT> {0:1} p.43
    ']
End Class

'This Structure is used within a source record To point To a name And address record Of the holder Of the
'source document. Formal And informal repository name And addresses are stored In the
'REPOSITORY_RECORD. Informal repositories include owner's of an unpublished work or of a rare
'published source, or a keeper Of personal collections. An example would be the owner Of a family Bible
'containing unpublished family genealogical entries. More formal repositories, such As the Family History
'Library, should show a call number of the source at that repository. The call number of that source
'should be recorded Using a subordinate CALN tag. Systems which Do Not use repository name And
'address record, should describe where the information cited Is stored In the <<NOTE_STRUCTURE>>
'of the REPOsitory source citation structure.
Public Class GED_SOURCE_REPOSITORY_CITATION
    'n REPO [ @XREF:REPO@ | <NULL>] {1:1} p.27
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
    '+1 CALN <SOURCE_CALL_NUMBER> {0:M} p.61
    '+2 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62
End Class

Public Class GED_SPOUSE_TO_FAMILY_LINK
    'n FAMS @<XREF:FAM>@ {1:1} p.24
    '+1 <<NOTE_STRUCTURE>> {0:M} p.37
End Class

'Primitive Elements Of the Lineage-Linked Form
'The field sizes show the minimum recommended field length within a database that Is constrained To fixed
'length fields. The field sizes are In addition To the GEDCOM level And tag overhead. GEDCOM lines are
'limited to 255 characters. However, the CONCatenation Or CONTinuation tags can be used to expand a
'field beyond this limit. CONT line implies that a New line should appear To preserve formatting. CONC
'implies concatenation To the previous line without a New line. This Is used so that a text note Or
'description can be processed (word wrapped) In a text window without fixed carriage returns. The
'CONT And CONC tags are being used to extend specified textual values.

Public Class ADDRESS_CITY
    ':= {Size=1:60}
    'The name Of the city used In the address. Isolated For sorting Or indexing.
End Class

Public Class ADDRESS_COUNTRY
    ':= {Size=1:60}
    'The name Of the country that pertains To the associated address. Isolated by some systems For sorting
    'Or indexing. Used in most cases to facilitate automatic sorting of mail.
End Class

Public Class ADDRESS_EMAIL
    ':= {Size=5:120}
    'An electronic address that can be used For contact such As an email address.
End Class

Public Class ADDRESS_FAX
    ':= {Size=5:60}
    'A FAX telephone number appropriate For sending data facsimiles.
End Class

Public Class ADDRESS_LINE
    ':= {Size=1:60}
    'Typically used To define a mailing address Of an individual When used subordinate To a RESIdent tag.
    'When it Is used subordinate to an event tag it Is the address of the place where the event took place.
    'The address lines usually contain the addressee's name and other street and city information so that it 
    'forms an address that meets mailing requirements.
End Class

Public Class ADDRESS_LINE1
    ':= {Size=1:60}
    'The first line Of the address used For indexing. This Is the value Of the line corresponding To the
    'ADDR tag line In the address Structure.
End Class

Public Class ADDRESS_LINE2
    ':= {Size=1:60}
    'The second line Of the address used For indexing. This Is the value Of the first CONT line subordinate
    'to the ADDR tag in the address structure.
End Class

Public Class ADDRESS_LINE3
    ':= {Size=1:60}
    'The third line Of the address used For indexing. This Is the value Of the second CONT line subordinate
    'to the ADDR tag in the address structure.
End Class

Public Class ADDRESS_POSTAL_CODE
    ':= {Size=1:10}
    'The ZIP Or postal code used by the various localities In handling Of mail. Isolated For sorting Or
    '42
    'indexing.
End Class

Public Class ADDRESS_STATE
    ':= {Size=1:60}
    'The name Of the state used In the address. Isolated For sorting Or indexing.
End Class

Public Class ADDRESS_WEB_PAGE
    ':= {Size=5:120}
    'The world wide web page address.
End Class

Public Class ADOPTED_BY_WHICH_PARENT
    ':= {Size=1:4}
    '[ HUSB | WIFE | BOTH ]
    'A code which shows which parent In the associated family record adopted this person.
    'Where:
    'HUSB = The HUSBand in the associated family adopted this person.
    'WIFE = The WIFE in the associated family adopted this person.
    'BOTH = Both HUSBand And WIFE adopted this person.
End Class

Public Class AGE_AT_EVENT
    ':= {Size=1:12}
    '[ < | > | <NULL>]
    '[ YYy MMm DDDd | YYy | MMm | DDDd |
    ' YYy MMm | YYy DDDd | MMm DDDd |
    ' CHILD | INFANT | STILLBORN ]
    ']
    'Where:
    '>= greater than indicated age
    '<= less than indicated age
    'y = a label indicating years
    'm = a label indicating months
    'd = a label indicating days
    'YY = number of full years
    'MM = number of months
    'DDD = number of days
    'CHILD = age < 8 years
    'INFANT = age < 1 year
    'STILLBORN = died just prior, at, Or near birth, 0 years
    'A number that indicates the age In years, months, And days that the principal was at the time Of the
    'associated Event. Any labels must come after their corresponding number, for example; 4y 8m 10d.
End Class

Public Class ANCESTRAL_FILE_NUMBER
    ':= {Size=1:12}
    'A unique permanent record number Of an individual record contained In the Family History
    'Department's Ancestral File.
End Class

Public Class APPROVED_SYSTEM_ID
    ':= {Size=1:20}
    '43
    'A system identification name which was obtained through the GEDCOM registration process. This
    'name must be unique from any other product. Spaces within the name must be substituted With a 0x5F
    '(underscore _) so as to create one word.
End Class

Public Class ATTRIBUTE_DESCRIPTOR
    ':= {Size=1:90}
    'Text describing a particular characteristic Or attribute assigned To an individual. This attribute value Is
    'assigned to the FACT tag. The classification of this specific attribute Or fact Is specified by the value
    'of the subordinate TYPE tag selected from the EVENT_DETAIL structure. For example if you were
    'classifying the skills a person had obtained; 
    '1 FACT Woodworking
    '2 TYPE Skills
End Class

Public Class ATTRIBUTE_TYPE
    ':= {Size=1:4}
    '[ CAST | EDUC | NATI | OCCU | PROP | RELI | RESI | TITL | FACT ]
    'An attribute which may have caused name, addresses, phone numbers, family listings To be recorded. 
    'Its application Is In helping To classify sources used For information.
End Class

Public Class AUTOMATED_RECORD_ID
    ':= {Size=1:12}
    'A unique record identification number assigned To the record by the source system. This number Is
    'intended to serve as a more sure means of identification of a record for reconciling differences in data
    'between two interfacing systems.
End Class

Public Class CASTE_NAME
    ':= {Size=1:90}
    'A name assigned To a particular group that this person was associated With, such As a particular racial
    'group, religious group, Or a group with an inherited status.
End Class

Public Class CAUSE_OF_EVENT
    ':= {Size=1:90}
    'Used in special cases to record the reasons which precipitated an event. Normally this will be used
    'subordinate to a death event to show cause of death, such as might be listed on a death certificate.
End Class

Public Class CERTAINTY_ASSESSMENT
    ':= {Size=1:1}
    '[ 0 | 1 | 2 | 3 ]
    'The QUAY tag's value conveys the submitter's quantitative evaluation of the credibility of a piece of
    'information, based upon its supporting evidence. Some systems use this feature to rank multiple
    'conflicting opinions For display Of most likely information first. It Is Not intended To eliminate the
    'receiver's need to evaluate the evidence for themselves.
    '0 = Unreliable evidence Or estimated data
    '1 = Questionable reliability of evidence (interviews, census, oral genealogies, Or potential for bias
    'For example, an autobiography)
    '2 = Secondary evidence, data officially recorded sometime after event
    '3 = Direct And primary evidence used, Or by dominance of the evidence
End Class

Public Class CHANGE_DATE
    ':= {Size=10:11}
End Class


'COUNT_OF_MARRIAGES:= {Size=13}
'The number Of different families that this person was known To have been a member Of As a spouse Or
'parent, regardless of whether the associated families are represented in the GEDCOM file.
'45
'Date:= {Size=4:35}
'[ <DATE_CALENDAR_ESCAPE> | <NULL>]
'<DATE_CALENDAR>
'        DATE_APPROXIMATED:= {Size=4:35}
'[
'ABT <DATE> |
'CAL <DATE> |
'EST <DATE>
']
'Where:
'ABT = About, meaning the date Is Not exact.
'CAL = Calculated mathematically, for example, from an event date And age.
'EST = Estimated based on an algorithm using some other event date.
'DATE_CALENDAR:= {Size=4:35}
'[ <DATE_GREG> | <DATE_JULN> | <DATE_HEBR> | <DATE_FREN> |
' <DATE_FUTURE> ]
'The selection Is based On the <DATE_CALENDAR_ESCAPE> that precedes the
'<DATE_CALENDAR> value immediately To the left. If <DATE_CALENDAR_ESCAPE> doesn't
'appear at this point, Then @#DGREGORIAN@ Is assumed. No future calendar types will use words
'(e.g., month names) from this list: FROM, TO, BEF, AFT, BET, And, ABT, EST, CAL, Or INT. 
'When only a day And month appears as a DATE value it Is considered a date phrase And Not a valid
'Date form.
'Date Escape Syntax Selected
' @#DGREGORIAN@ <DATE_GREG>
' @#DJULIAN@ <DATE_JULN>
' @#DHEBREW@ <DATE_HEBR>
' @#DFRENCH R@ <DATE_FREN>
' @#DROMAN@ for future definition
' @#DUNKNOWN@ calendar Not known
'DATE_CALENDAR_ESCAPE:= {Size=4:15} 
'[ @#DHEBREW@ | @#DROMAN@ | @#DFRENCH R@ | @#DGREGORIAN@ |
' @#DJULIAN@ | @#DUNKNOWN@ ]
'The date escape determines the date interpretation by signifying which <DATE_CALENDAR> to use.
'The Default calendar Is the Gregorian calendar.
'DATE_EXACT:= {Size=10:11}
'<Day> <MONTH> <YEAR_GREG>
'        DATE_FREN:= {Size=4:35}
'[ <YEAR>[B.C.] | <MONTH_FREN> <YEAR> |
'46
' <Day> <MONTH_FREN> <YEAR> ]
'See <MONTH_FREN> page 53
'DATE_GREG:= {Size=4:35}
'[ <YEAR_GREG>[B.C.] | <MONTH> <YEAR_GREG> |
' <Day> <MONTH> <YEAR_GREG> ]
'See <YEAR_GREG> page 65.
'DATE_HEBR:= {Size=4:35}
'[ <YEAR>[B.C.] | <MONTH_HEBR> <YEAR> |
' <Day> <MONTH_HEBR> <YEAR> ]
'See <MONTH_HEBR> page 53
'DATE_JULN:= {Size=4:35}
'[ <YEAR>[B.C.] | <MONTH> <YEAR> | <DAY> <MONTH> <YEAR> ]
'DATE_LDS_ORD:= {Size=4:35}
'<DATE_VALUE>
'        LDS ordinance dates use only the Gregorian Date And most often use the form Of day, month, And
'year. Only in rare instances Is there a partial date. The temple tag And code should always accompany
'temple ordinance dates. Sometimes the LDS_(ordinance)_DATE_STATUS Is used To indicate that an
'ordinance date And temple code Is Not required, such as when BIC Is used. (See
'LDS_(ordinance)_DATE_STATUS definitions beginning on page 51.)
'DATE_PERIOD:= {Size=7:35}
'[
'FROM <DATE> |
'TO <DATE> |
'FROM <DATE> TO <DATE>
']
'Where:
'FROM = Indicates the beginning of a happening Or state.
'TO = Indicates the ending of a happening Or state.
'Examples:
'FROM 1904 to 1915
'= The state of some attribute existed from 1904 to 1915 inclusive.
'FROM 1904
'= The state of the attribute began in 1904 but the end date Is unknown.
'TO 1915
'= The state ended in 1915 but the begin date Is unknown.
'47
'DATE_PHRASE:= {Size=1:35}
'<Text>
'        Any statement offered As a Date When the year Is Not recognizable To a Date parser, but which gives
'information about When an Event occurred.
'DATE_RANGE:= {Size=8:35}
'[
'BEF <DATE> |
'AFT <DATE> |
'BET <DATE> And <DATE>
']
'Where:
'AFT = Event happened after the given date.
'BEF = Event happened before the given date.
'BET = Event happened some time between date 1 And date 2. For example, bet 1904 And 1915
'indicates that the Event state (perhaps a Single day) existed somewhere between 1904 And
'1915 inclusive.
'The date range differs from the date period in that the date range Is an estimate that an event happened
'On a single date somewhere in the date range specified.
'The following are equivalent And interchangeable
'Short form Long Form
'1852 BET 1 JAN 1852 And 31 DEC 1852
'1852 BET 1 JAN 1852 And DEC 1852
'1852 BET JAN 1852 And 31 DEC 1852
'1852 BET JAN 1852 And DEC 1852
'JAN 1920 BET 1 JAN 1920 And 31 JAN 1920
'DATE_VALUE:= {Size=1:35}
'[
'<DATE> |
'<DATE_PERIOD> |
'<DATE_RANGE>|
'<DATE_APPROXIMATED> |
'INT <DATE> (<DATE_PHRASE>) |
'(<DATE_PHRASE>)
']
'The DATE_VALUE represents the Date Of an activity, attribute, Or Event where:
'INT = Interpreted from knowledge about the associated date phrase included in parentheses. 
'48
'An acceptable alternative To the Date phrase choice Is To use one Of the other choices such As
'<DATE_APPROXIMATED> choice As the Date line value And Then include the Date phrase value
'as a NOTE value subordinate to the DATE line tag.
'The date value can take on the date form of just a date, an approximated date, between a date And
'another date, And from one date to another date. The preferred form of showing date imprecision, Is
'to show, for example, MAY 1890 rather than ABT 12 MAY 1890. This Is because limits have Not
'been assigned To the precision Of the prefixes such As ABT Or EST. 
'DAY:= {Size=1:2}
'dd
'Day of the month, where dd Is a numeric digit whose value Is within the valid range of the days for the
'associated calendar month.
'DESCRIPTIVE_TITLE:= {Size=1:248} 
'The title Of a work, record, item, Or Object.
'DIGIT:= {Size=1:1}
'A single digit (0-9).
'ENTRY_RECORDING_DATE:= {Size=1:90}
'<DATE_VALUE>
'        The Date that this Event data was entered into the original source document.
'EVENT_ATTRIBUTE_TYPE:= {Size=1:15}
'[ <EVENT_TYPE_INDIVIDUAL> |
' <EVENT_TYPE_FAMILY> |
' <ATTRIBUTE_TYPE> ]
'A code that classifies the principal event or happening that caused the source record entry to be
'created. If the event or attribute doesn't translate to one of these tag codes, then a user supplied value
'is expected and will be generally classified in the category of other.
'EVENT_DESCRIPTOR:= {Size=1:90}
'Text describing a particular event pertaining to the individual or family. This event value is usually
'assigned to the EVEN tag. The classification as to the difference between this specific event and other
'occurrences of the EVENt tag is indicated by the use of a subordinate TYPE tag selected from the
'EVENT_DETAIL structure. For example; 
'1 EVEN Appointed Zoning Committee Chairperson
'2 TYPE Civic Appointments
'2 DATE FROM JAN 1952 TO JAN 1956
'2 PLAC Cove, Cache, Utah
'2 AGNC Cove City Redevelopment
'49
'EVENT_OR_FACT_CLASSIFICATION:= {Size=1:90}
'A descriptive word or phrase used to further classify the parent event or attribute tag. This should be
'used whenever either of the generic EVEN or FACT tags are used. The value of this primative is
'responsible for classifying the generic event or fact being cited. For example, if the attribute being
'defined was one of the persons skills, such as woodworking, the FACT tag would have the value of
'`Woodworking', followed by a subordinate TYPE tag with the value `Skills.'
'1 FACT Woodworking
'2 TYPE Skills
'This groups the fact into a generic skills attribute, and in particular this entry records the fact that this
'individual possessed the skill of woodworking. Using the subordinate TYPE tag classification method
'with any of the other defined event tags provides a further classification of the parent tag but does not
'change the basic meaning of the parent tag. For example, a MARR tag could be subordinated with a
'TYPE tag with an EVENT_DESCRIPTOR value of `Common Law.' 
'1 MARR
'2 TYPE Common Law
'This classifies the entry as a common law marriage but the event is still a marriage event. Other
'descriptor values might include, for example,`stillborn' as a qualifier to BIRTh or `Tribal Custom' as a
'qualifier to MARRiage.
'EVENT_TYPE_CITED_FROM:= {SIZE=1:15}
'[ <EVENT_ATTRIBUTE_TYPE> ]
'A code that indicates the type of event which was responsible for the source entry being recorded. For
'example, if the entry was created to record a birth of a child, then the type would be BIRT regardless
'of the assertions made from that record, such as the mother's name or mother's birth date. This will
'allow a prioritized best view choice and a determination of the certainty associated with the source
'used in asserting the cited fact.
'EVENT_TYPE_FAMILY:= {Size=3:4}
'[ ANUL | CENS | DIV | DIVF | ENGA | MARR |
' MARB | MARC | MARL | MARS | EVEN ]
'A code used to indicate the type of family event. The definition is the same as the corresponding
'event tag defined in Appendix A. (See Appendix A, starting on page 83).
'EVENT_TYPE_INDIVIDUAL:= {Size=3:4}
'[ ADOP | BIRT | BAPM | BARM | BASM |
' BLES | BURI | CENS | CHR | CHRA |
' CONF | CREM | DEAT | EMIG | FCOM |
' GRAD | IMMI | NATU | ORDN |
' RETI | PROB | WILL | EVEN ]
'50
'A code used to indicate the type of family event. The definition is the same as the corresponding
'event tag defined in Appendix A. (See Appendix A, starting on page 83).
'EVENTS_RECORDED:= {Size=1:90}
'[<EVENT_ATTRIBUTE_TYPE> |
' <EVENTS_RECORDED>, <EVENT_ATTRIBUTE_TYPE>]
'An enumeration of the different kinds of events that were recorded in a particular source. Each
'enumeration is separated by a comma. Such as a parish register of births, deaths, and marriages would
'be BIRT, DEAT, MARR.
'FILE_NAME:= {Size=1:90}
'The name of the GEDCOM transmission file. If the file name includes a file extension it must be
'shown in the form (filename.ext).
'GEDCOM_CONTENT_DESCRIPTION:= {Size=1:248}
'A note that a user enters to describe the contents of the lineage-linked file in terms of "ancestors or
'descendants of" so that the person receiving the data knows what genealogical information the
'transmission contains.
'GEDCOM_FORM:= {Size=14:20}
'[ LINEAGE-LINKED ]
'The GEDCOM form used to construct this transmission. There maybe other forms used such as
'CommSoft's "EVENT_LINEAGE_LINKED" but these specifications define only the LINEAGELINKED Form. Systems will use this value to specify GEDCOM compatible with these
'specifications.
'GENERATIONS_OF_ANCESTORS:= {Size=1:4}
'The number of generations of ancestors included in this transmission. This value is usually provided
'when FamilySearch programs build a GEDCOM file for a patron requesting a download of ancestors.
'GENERATIONS_OF_DESCENDANTS:= {Size=1:4}
'The number of generations of descendants included in this transmission. This value is usually provided
'when FamilySearch programs build a GEDCOM file for a patron requesting a download of
'descendants.
'LANGUAGE_ID:= {Size=1:15}
'A table of valid latin language identification codes.
'[ Afrikaans | Albanian | Anglo-Saxon | Catalan | Catalan_Spn | Czech | Danish | Dutch | English |
'Esperanto | Estonian | Faroese | Finnish | French | German | Hawaiian | Hungarian | Icelandic |
'Indonesian | Italian | Latvian | Lithuanian | Navaho | Norwegian | Polish | Portuguese | Romanian |
'Serbo_Croa | Slovak | Slovene | Spanish | Swedish | Turkish | Wendic ]
'Other languages not supported until UNICODE
'51
'[ Amharic | Arabic | Armenian | Assamese | Belorusian | Bengali | Braj | Bulgarian | Burmese |
'Cantonese | Church-Slavic | Dogri | Georgian | Greek | Gujarati | Hebrew | Hindi | Japanese |
'Kannada | Khmer | Konkani | Korean | Lahnda | Lao | Macedonian | Maithili | Malayalam | Mandrin |
'Manipuri | Marathi | Mewari | Nepali | Oriya | Pahari | Pali | Panjabi | Persian | Prakrit | Pusto |
'Rajasthani | Russian | Sanskrit | Serb | Tagalog | Tamil | Telugu | Thai | Tibetan | Ukrainian | Urdu |
'Vietnamese | Yiddish ]
'LANGUAGE_OF_TEXT:= {Size=1:15}
'[ <LANGUAGE_ID> ]
'The human language in which the data in the transmission is normally read or written. It is used
'primarily by programs to select language-specific sorting sequences and phonetic name matching
'algorithms.
'LANGUAGE_PREFERENCE:= {Size=1:90}
'[ <LANGUAGE_ID> ]
'The language in which a person prefers to communicate. Multiple language preference is shown by
'using multiple occurrences in order of priority.
'LDS_BAPTISM_DATE_STATUS:= {Size=5:10}
'[ CHILD | COMPLETED | EXCLUDED | PRE-1970 |
' STILLBORN | SUBMITTED | UNCLEARED ]
'A code indicating the status of an LDS baptism and confirmation date where:
'CHILD = Died before becoming eight years old, baptism not required.
'COMPLETED = Completed but the date is not known.
'EXCLUDED = Patron excluded this ordinance from being cleared in this submission.
'PRE-1970 = Ordinance is likely completed, another ordinance for this person was converted
'from temple records of work completed before 1970, therefore this ordinance is
'assumed to be complete until all records are converted.
'STILLBORN = Stillborn, baptism not required.
'SUBMITTED = Ordinance was previously submitted.
'UNCLEARED = Data for clearing ordinance request was insufficient.
'LDS_CHILD_SEALING_DATE_STATUS:= {Size=5:10}
'[ BIC | COMPLETED | EXCLUDED | DNS | PRE-1970 |
' STILLBORN | SUBMITTED | UNCLEARED ]
'BIC = Born in the covenant receiving blessing of child to parent sealing.
'EXCLUDED = Patron excluded this ordinance from being cleared in this submission.
'PRE-1970 = (See pre-1970 under LDS_BAPTISM_DATE_STATUS on page 51.)
'STILLBORN = Stillborn, not required.
'52
'SUBMITTED = Ordinance was previously submitted.
'UNCLEARED = Data for clearing ordinance request was insufficient.
'LDS_ENDOWMENT_DATE_STATUS:= {Size=5:10}
'[ CHILD | COMPLETED | EXCLUDED | PRE-1970 |
' STILLBORN | SUBMITTED | UNCLEARED ]
'A code indicating the status of an LDS endowment ordinance where:
'CHILD = Died before eight years old.
'COMPLETED = Completed but the date is not known.
'EXCLUDED = Patron excluded this ordinance from being cleared in this submission.
'INFANT = Died before less than one year old, baptism or endowment not required.
'PRE-1970 = (See pre-1970 under LDS_BAPTISM_DATE_STATUS on page 51.)
'STILLBORN = Stillborn, ordinance not required.
'SUBMITTED = Ordinance was previously submitted.
'UNCLEARED = Data for clearing ordinance request was insufficient.
'LDS_SPOUSE_SEALING_DATE_STATUS:= {Size=3:10}
'[ CANCELED | COMPLETED | DNS | EXCLUDED |
' DNS/CAN | PRE-1970 | SUBMITTED | UNCLEARED ]
'CANCELED = Canceled and considered invalid.
'COMPLETED = Completed but the date is not known.
'EXCLUDED = Patron excluded this ordinance from being cleared in this submission.
'DNS = This ordinance is not authorized.
'DNS/CAN = This ordinance is not authorized, previous sealing cancelled.
'PRE-1970 = (See pre-1970 under LDS_BAPTISM_DATE_STATUS on page 51.)
'SUBMITTED = Ordinance was previously submitted.
'UNCLEARED = Data for clearing ordinance request was insufficient.
'MONTH:= {Size=3}
'[ JAN | FEB | MAR | APR | MAY | JUN |
' JUL | AUG | SEP | OCT | NOV | DEC ]
'Where:
'JAN = January
'FEB = February
'MAR = March
'APR = April
'MAY = May
'JUN = June
'JUL = July
'AUG = August
'53
'SEP = September
'OCT = October
'NOV = November
'DEC = December
'MONTH_FREN:= {Size=4}
'[ VEND | BRUM | FRIM | NIVO | PLUV | VENT | GERM |
' FLOR | PRAI | MESS | THER | FRUC | COMP ]
'Where:
'VEND = VENDEMIAIRE
'BRUM = BRUMAIRE
'FRIM = FRIMAIRE
'NIVO = NIVOSE
'PLUV = PLUVIOSE
'VENT = VENTOSE
'GERM = GERMINAL
'FLOR = FLOREAL
'PRAI = PRAIRIAL
'MESS = MESSIDOR
'THER = THERMIDOR
'FRUC = FRUCTIDOR
'COMP = JOUR_COMPLEMENTAIRS
'MONTH_HEBR:= {Size=3}
'[ TSH | CSH | KSL | TVT | SHV | ADR | ADS |
' NSN | IYR | SVN | TMZ | AAV | ELL ]
'Where:
'TSH = Tishri
'CSH = Cheshvan
'KSL = Kislev
'TVT = Tevet
'SHV = Shevat
'ADR = Adar
'ADS = Adar Sheni
'NSN = Nisan
'IYR = Iyar
'SVN = Sivan
'TMZ = Tammuz
'AAV = Av
'ELL = Elul
'54
'MULTIMEDIA_FILE_REFERENCE:= {Size=1:30}
'A complete local or remote file reference to the auxiliary data to be linked to the GEDCOM context. 
'Remote reference would include a network address where the multimedia data may be obtained.
'MULTIMEDIA_FORMAT:= {Size=3:4}
'[ bmp | gif | jpg | ole | pcx | tif | wav ]
'Indicates the format of the multimedia data associated with the specific GEDCOM context. This
'allows processors to determine whether they can process the data object. Any linked files should
'contain the data required, in the indicated format, to process the file data.
'NAME_OF_BUSINESS:= {Size=1:90}
'Name of the business, corporation, or person that produced or commissioned the product.
'NAME_OF_FAMILY_FILE:= {Size=1:120}
'Name under which family names for ordinances are stored in the temple's family file.
'NAME_OF_PRODUCT:= {Size=1:90}
'The name of the software product that produced this transmission.
'NAME_OF_REPOSITORY:= {Size=1:90}
'The official name of the archive in which the stated source material is stored.
'NAME_OF_SOURCE_DATA:= {Size=1:90}
'The name of the electronic data source that was used to obtain the data in this transmission. For
'example, the data may have been obtained from a CD-ROM disc that was named "U.S. 1880
'CENSUS CD-ROM vol. 13."
'NAME_PERSONAL:= {Size=1:120}
'[
' <NAME_TEXT> |
'/<NAME_TEXT>/ |
' <NAME_TEXT> /<NAME_TEXT>/ |
'/<NAME_TEXT>/ <NAME_TEXT> |
' <NAME_TEXT> /<NAME_TEXT>/ <NAME_TEXT>
']
'The surname of an individual, if known, is enclosed between two slash (/) characters. The order of the
'name parts should be the order that the person would, by custom of their culture, have used when
'giving it to a recorder. Early versions of Personal Ancestral File ®
' and other products did not use the
'trailing slash when the surname was the last element of the name. If part of name is illegible, that part
'is indicated by an ellipsis (...). Capitalize the name of a person or place in the conventional
'manner— capitalize the first letter of each part and lowercase the other letters, unless conventional
'usage is otherwise. For example: McMurray. 
'55
'Examples:
'William Lee (given name only or surname not known)
'/Parry/ (surname only)
'William Lee /Parry/
'William Lee /Mac Parry/ (both parts (Mac and Parry) are surname parts
'William /Lee/ Parry (surname imbedded in the name string)
'William Lee /Pa.../
'NAME_PHONETIC_VARIATION:= {Size=1:120}
'The phonetic variation of the name is written in the same form as the was the name used in the
'superior <NAME_PERSONAL> primitive, but phonetically written using the method indicated by the
'subordinate <PHONETIC_TYPE> value, for example if hiragana was used to provide a reading of a
'name written in kanji, then the <PHONETIC_TYPE> value would indicate ‘kana’. See page 57.
'NAME_PIECE:= {Size=1:90}
'The piece of the name pertaining to the name part of interest. The surname part, the given name part,
'the name prefix part, or the name suffix part.
'NAME_PIECE_GIVEN:= {Size=1:120}
'[ <NAME_PIECE> | <NAME_PIECE_GIVEN>, <NAME_PIECE> ]
'Given name or earned name. Different given names are separated by a comma.
'NAME_PIECE_NICKNAME:= {Size=1:30}
'[ <NAME_PIECE> | <NAME_PIECE_NICKNAME>, <NAME_PIECE> ]
'A descriptive or familiar name used in connection with one's proper name.
'NAME_PIECE_PREFIX:= {Size=1:30}
'[ <NAME_PIECE> | <NAME_PIECE_PREFIX>, <NAME_PIECE> ]
'Non indexing name piece that appears preceding the given name and surname parts. Different name
'prefix parts are separated by a comma.
'For example:
'Lt. Cmndr. Joseph /Allen/ jr.
'In this example Lt. Cmndr. is considered as the name prefix portion.
'NAME_PIECE_SUFFIX:= {Size=1:30}
'[ <NAME_PIECE> | <NAME_PIECE_SUFFIX>, <NAME_PIECE> ]
'Non-indexing name piece that appears after the given name and surname parts. Different name suffix
'parts are separated by a comma.
'For example:
'Lt. Cmndr. Joseph /Allen/ jr.
'In this example jr. is considered as the name suffix portion.
'NAME_PIECE_SURNAME:= {Size=1:120}
'56
'[ <NAME_PIECE> | <NAME_PIECE_SURNAME>, <NAME_PIECE> ]
'Surname or family name. Different surnames are separated by a comma.
'NAME_PIECE_SURNAME_PREFIX:= {Size=1:30}
'[ <NAME_PIECE> | <NAME_PIECE_SURNAME_PREFIX>, <NAME_PIECE> ]
'Surname prefix or article used in a family name. Different surname articles are separated by a comma,
'for example in the name "de la Cruz", this value would be "de, la".
'NAME_ROMANIZED_VARIATION:= {Size=1:120}
'The romanized variation of the name is written in the same form prescribed for the name used in the
'superior <NAME_PERSONAL> context. The method used to romanize the name is indicated by the
'line_value of the subordinate <ROMANIZED_TYPE>, for example if romaji was used to provide a
'reading of a name written in kanji, then the ROMANIZED_TYPE subordinate to the ROMN tag
'would indicate romaji. See page 61. 
'NAME_TEXT:= {Size=1:120}
'<TEXT> excluding commas, numbers, special characters not considered diacritics. 
'NAME_TYPE:= {Size=5:30}
' [ aka | birth | immigrant | maiden | married | <user defined>]
'Indicates the name type, for example the name issued or assumed as an immigrant.
'aka = also known as, alias, etc.
'birth = name given on birth certificate.
'immigrant = name assumed at the time of immigration.
'maiden = maiden name, name before first marriage.
'married = name was persons previous married name.
'user_defined= other text name that defines the name type.
'NATIONAL_ID_NUMBER:= {Size=1:30}
'A nationally-controlled number assigned to an individual. Commonly known national numbers should
'be assigned their own tag, such as SSN for U.S. Social Security Number. The use of the IDNO tag
'requires a subordinate TYPE tag to identify what kind of number is being stored.
'For example:
'n IDNO 43-456-1899
'+1 TYPE Canadian Health Registration
'NATIONAL_OR_TRIBAL_ORIGIN:= {Size=1:120}
'The person's division of national origin or other folk, house, kindred, lineage, or tribal interest.
'Examples: Irish, Swede, Egyptian Coptic, Sioux Dakota Rosebud, Apache Chiricawa, Navajo Bitter
'Water, Eastern Cherokee Taliwa Wolf, and so forth.
'NEW_TAG:= {Size=1:15}
'A user-defined tag that is contained in the GEDCOM current transmission. This tag must begin with
'57
'an underscore (_) and should only be interpreted in the context of the sending system.
'NOBILITY_TYPE_TITLE:= {Size=1:120}
'The title given to or used by a person, especially of royalty or other noble class within a locality.
'NULL:= {Size=0:0}
'A convention that indicates the absence of any 8-bit ASCII character in the value including the null
'character (0x00) which is prohibited.
'NUMBER:=
'[<DIGIT> | <NUMBER>+<DIGIT>]
'OCCUPATION:= {Size=1:90}
'The kind of activity that an individual does for a job, profession, or principal activity.
'ORDINANCE_PROCESS_FLAG:= {Size=2:3}
'[ yes | no ]
'A flag that indicates whether submission should be processed for clearing temple ordinances. 
'PEDIGREE_LINKAGE_TYPE:= {Size=5:7}
'[ adopted | birth | foster | sealing ]
'A code used to indicate the child to family relationship for pedigree navigation purposes.
'Where:
'adopted = indicates adoptive parents.
'birth = indicates birth parents.
'foster = indicates child was included in a foster or guardian family.
'sealing = indicates child was sealed to parents other than birth parents. 
'PERMANENT_RECORD_FILE_NUMBER:= {Size=1:90} 
'<REGISTERED_RESOURCE_IDENTIFIER>:<RECORD_IDENTIFIER>
'The record number that uniquely identifies this record within a registered network resource. The
'number will be usable as a cross-reference pointer. The use of the colon (:) is reserved to indicate the
'separation of the "registered resource identifier" (which precedes the colon) and the unique "record
'identifier" within that resource (which follows the colon). If the colon is used, implementations that
'check pointers should not expect to find a matching cross-reference identifier in the transmission but
'would find it in the indicated database within a network. Making resource files available to a public
'network is a future implementation.
'PHONE_NUMBER:= {Size=1:25}
'A phone number.
'PHONETIC_TYPE:= {Size=5:30}
'[<user defined> | hangul | kana]
'58
'Indicates the method used in transforming the text to the phonetic variation.
'<user define> record method used to arrive at the phonetic variation of the name.
'hangul Phonetic method for sounding Korean glifs.
'kana Hiragana and/or Katakana characters were used in sounding the Kanji character used by
'japanese
'PHYSICAL_DESCRIPTION:= {Size=1:248}
'An unstructured list of the attributes that describe the physical characteristics of a person, place, or
'object. Commas separate each attribute.
'Example:
'1 DSCR Hair Brown, Eyes Brown, Height 5 ft 8 in
'2 DATE 23 JUL 1935
'PLACE_HIERARCHY:= {Size=1:120}
'This shows the jurisdictional entities that are named in a sequence from the lowest to the highest
'jurisdiction. The jurisdictions are separated by commas, and any jurisdiction's name that is missing is
'still accounted for by a comma. When a PLAC.FORM structure is included in the HEADER of a
'GEDCOM transmission, it implies that all place names follow this jurisdictional format and each
'jurisdiction is accounted for by a comma, whether the name is known or not. When the PLAC.FORM
'is subordinate to an event, it temporarily overrides the implications made by the PLAC.FORM
'structure stated in the HEADER. This usage is not common and, therefore, not encouraged. It should
'only be used when a system has over-structured its place-names.
'PLACE_LATITUDE:= {Size=5:8}
'The value specifying the latitudinal coordinate of the place name. The latitude coordinate is the
'direction North or South from the equator in degrees and fraction of degrees carried out to give the 
'desired accuracy. For example: 18 degrees, 9 minutes, and 3.4 seconds North would be formatted as
'N18.150944. Minutes and seconds are converted by dividing the minutes value by 60 and the seconds
'value by 3600 and adding the results together. This sum becomes the fractional part of the degree’s
'value.
'PLACE_LIVING_ORDINANCE:= {Size=1:120}
'<PLACE_NAME>
'The locality of the place where a living LDS ordinance took place. Typically, a living LDS baptism
'place would be recorded in this field.
'PLACE_LONGITUDE:= {Size=5:8}
'The value specifying the longitudinal coordinate of the place name. The longitude coordinate is 
'Degrees and fraction of degrees east or west of the zero or base meridian coordinate. For example:
'168 degrees, 9 minutes, and 3.4 seconds East would be formatted as E168.150944. 
'PLACE_NAME:= {Size=1:120}
'[
'59
' <PLACE_TEXT> | 
' <PLACE_TEXT>, <PLACE_NAME> 
'] 
'The jurisdictional name of the place where the event took place. Jurisdictions are separated by
'commas, for example, "Cove, Cache, Utah, USA." If the actual jurisdictional names of these places
'have been identified, they can be shown using a PLAC.FORM structure either in the HEADER or in
'the event structure. (See <PLACE_HIERARCHY>, page 58.)
'PLACE_PHONETIC_VARIATION:= {Size=1:120}
'The phonetic variation of the place name is written in the same form as was the place name used in
'the superior <PLACE_NAME> primitive, but phonetically written using the method indicated by the
'subordinate <PHONETIC_TYPE> value, for example if hiragana was used to provide a reading of a a
'name written in kanji, then the <PHONETIC_TYPE> value would indicate kana. (See
'<PHONETIC_TYPE> page 57.)
'PLACE_ROMANIZED_VARIATION:= {Size=1:120}
'The romanized variation of the place name is written in the same form prescribed for the place name
'used in the superior <PLACE_NAME> context. The method used to romanize the name is indicated
'by the line_value of the subordinate <ROMANIZED_TYPE>, for example if romaji was used to
'provide a reading of a place name written in kanji, then the <ROMANIZED_TYPE> subordinate to
'the ROMN tag would indicate ‘romaji’. (See <ROMANIZED_TYPE> page 61.)
'PLACE_TEXT:= {Size=1:120}
'<TEXT> excluding the comma(s).
'POSSESSIONS:= {Size=1:248}
'A list of possessions (real estate or other property) belonging to this individual.
'PUBLICATION_DATE:= {Size=10:11}
'<DATE_EXACT>
'The date this source was published or created.
'RECEIVING_SYSTEM_NAME:= {Size=1:20}
'The name of the system expected to process the GEDCOM-compatible transmission. The registered
'RECEIVING_SYSTEM_NAME for all GEDCOM submissions to the Family History Department
'must be one of the following names:
'! "ANSTFILE" when submitting to Ancestral File.
'! "TempleReady" when submitting for temple ordinance clearance.
'RECORD_IDENTIFIER:= {Size=1:18}
'An identification number assigned to each record within a specific database. The database to which the
'RECORD_IDENTIFIER pertains is indicated by the REGISTERED_RESOURCE_NUMBER which
'60
'precedes the colon (:). If the RECORD_IDENTIFIER is not preceded by a colon, it is a reference to a
'record within the current GEDCOM transmission.
'REGISTERED_RESOURCE_IDENTIFIER:= {Size=1:25}
'This is an identifier assigned to a resource database that is available through access to a network. This
'is for future GEDCOM releases.
'RELATION_IS_DESCRIPTOR:= {Size=1:25}
'A word or phrase that states object 1's relation is object 2. For example you would read the following
'as "Joe Jacob's great grandson is the submitter pointed to by the @XREF:SUBM@":
'0 INDI
'1 NAME Joe /Jacob/
'1 ASSO @<XREF:SUBM>@
'2 RELA great grandson
'RELIGIOUS_AFFILIATION:= {Size=1:90}
'A name of the religion with which this person, event, or record was affiliated.
'RESPONSIBLE_AGENCY:= {Size=1:120}
'The organization, institution, corporation, person, or other entity that has responsibility for the
'associated context. For example, an employer of a person of an associated occupation, or a church
'that administered rites or events, or an organization responsible for creating and/or archiving records.
'RESTRICTION_NOTICE:= {Size=6:7}
'[confidential | locked | privacy ]
'The restriction notice is defined for Ancestral File usage. Ancestral File download GEDCOM files
'may contain this data.
'Where:
'confidential = This data was marked as confidential by the user. In some systems data marked as
'confidential will be treated differently, for example, there might be an option that
'would stop confidential data from appearing on printed reports or would prevent that
'information from being exported.
'locked = Some records in Ancestral File have been satisfactorily proven by evidence, but
'because of source conflicts or incorrect traditions, there are repeated attempts to
'change this record. By arrangement, the Ancestral File Custodian can lock a record so
'that it cannot be changed without an agreement from the person assigned as the
'steward of such a record. The assigned steward is either the submitter listed for the
'record or Family History Support when no submitter is listed.
'privacy = Indicate that information concerning this record is not present due to rights of or an
'approved request for privacy. For example, data from requested downloads of the
'61
'Ancestral File may have individuals marked with ‘privacy’ if they are assumed living,
'that is they were born within the last 110 years and there isn’t a death date. In certain
'cases family records may also be marked with the RESN tag of privacy if either
'individual acting in the role of HUSB or WIFE is assumed living.
'ROLE_DESCRIPTOR:= {Size=1:25} 
'A word or phrase that identifies a person's role in an event being described. This should be the same
'word or phrase, and in the same language, that the recorder used to define the role in the actual
'record.
'ROLE_IN_EVENT:= {Size=1:15}
'[ CHIL | HUSB | WIFE | MOTH | FATH | SPOU | (<ROLE_DESCRIPTOR>) ]
'Indicates what role this person played in the event that is being cited in this context. For example, if
'you cite a child's birth record as the source of the mother's name, the value for this field is "MOTH." If
'you describe the groom of a marriage, the role is "HUSB." If the role is something different than one
'of the six relationship role tags listed above then enclose the role name within matching parentheses.
'ROMANIZED_TYPE:= {Size=5:30}
' [<user defined> | pinyin | romaji | wadegiles]
'Indicates the method used in transforming the text to a romanized variation.
'SCHOLASTIC_ACHIEVEMENT:= {Size=1:248}
'A description of a scholastic or educational achievement or pursuit.
'SEX_VALUE:= {Size=1:7}
'A code that indicates the sex of the individual:
'M = Male 
'F = Female
'U = Undetermined from available records and quite sure that it can’t be.
'SOCIAL_SECURITY_NUMBER:= {Size=9:11}
'A number assigned to a person in the United States for identification purposes.
'SOURCE_CALL_NUMBER:= {Size=1:120}
'An identification or reference description used to file and retrieve items from the holdings of a
'repository.
'SOURCE_DESCRIPTION:= {Size=1:248}
'A free form text block used to describe the source from which information was obtained. This text
'block is used by those systems which cannot use a pointer to a source record. It must contain a
'descriptive title, who created the work, where and when it was created, and where the source data
'stored. The developer should encourage users to use an appropriate style for forming this free form
'bibliographic reference. Developers are encouraged to support the SOURCE_RECORD method of
'62
'reporting bibliographic reference descriptions. 
'SOURCE_DESCRIPTIVE_TITLE:= {Size=1:248} 
'The title of the work, record, or item and, when appropriate, the title of the larger work or series of
'which it is a part.
'For a published work, a book for example, might have a title plus the title of the series of which the
'book is a part. A magazine article would have a title plus the title of the magazine that published the
'article.
'For An unpublished work, such as:
'! A letter might include the date, the sender, and the receiver.
'! A transaction between a buyer and seller might have their names and the transaction date.
'! A family Bible containing genealogical information might have past and present owners and a
'physical description of the book. 
'! A personal interview would cite the informant and interviewer.
'SOURCE_FILED_BY_ENTRY:= {Size= 1:60}
'This entry is to provide a short title used for sorting, filing, and retrieving source records.
'SOURCE_JURISDICTION_PLACE:= {Size=1:120}
'<PLACE_NAME>
'The name of the lowest jurisdiction that encompasses all lower-level places named in this source. For
'example, "Oneida, Idaho" would be used as a source jurisdiction place for events occurring in the
'various towns within Oneida County. "Idaho" would be the source jurisdiction place if the events
'recorded took place in other counties as well as Oneida County.
'SOURCE_MEDIA_TYPE:= {Size=1:15}
'[ audio | book | card | electronic | fiche | film | magazine |
' manuscript | map | newspaper | photo | tombstone | video ]
'A code, selected from one of the media classifications choices above, that indicates the type of
'material in which the referenced source is stored.
'SOURCE_ORIGINATOR:= {Size=1:248}
'The person, agency, or entity who created the record. For a published work, this could be the author,
'compiler, transcriber, abstractor, or editor. For an unpublished source, this may be an individual, a
'government agency, church organization, or private organization, etc.
'SOURCE_PUBLICATION_FACTS:= {Size=1:248}
'When and where the record was created. For published works, this includes information such as the
'city of publication, name of the publisher, and year of publication.
'For an unpublished work, it includes the date the record was created and the place where it was
'63
'created. For example, the county and state of residence of a person making a declaration for a pension
'or the city and state of residence of the writer of a letter.
'SUBMITTER_NAME:= {Size=1:60}
'The name of the submitter formatted for display and address generation.
'SUBMITTER_REGISTERED_RFN:= {Size=1:30}
'A registered number of a submitter of Ancestral File data. This number is used in subsequent
'submissions or inquiries by the submitter for identification purposes.
'SUBMITTER_TEXT:= {Size=1:248}
'Comments or opinions from the submitter.
'TEMPLE_CODE:= {Size=4:5}
'An abbreviation of the temple in which LDS temple ordinances were performed. (See Appendix B,
'page 96.)
'TEXT:= {Size=1:248}
'A string composed of any valid character from the GEDCOM character set.
'TEXT_FROM_SOURCE:= {Size=1:248}
'<TEXT>
'A verbatim copy of any description contained within the source. This indicates notes or text that are
'actually contained in the source document, not the submitter's opinion about the source. This should
'be, from the evidence point of view, "what the original record keeper said" as opposed to the
'researcher's interpretation. The word TEXT, in this case, means from the text which appeared in the
'source record including labels.
'TIME_VALUE:= {Size=1:12}
'[ hh:mm:ss.fs ]
'The time of a specific event, usually a computer-timed event, where:
'hh = hours on a 24-hour clock
'mm = minutes
'ss = seconds (optional)
'fs = decimal fraction of a second (optional)
'TRANSMISSION_DATE:= {Size=10:11}
'<DATE_EXACT>
'The date that this transmission was created.
'USER_REFERENCE_NUMBER:= {Size=1:20}
'A user-defined number or text that the submitter uses to identify this record. For instance, it may be a
'record number within the submitter's automated or manual system, or it may be a page and position
'64
'number on a pedigree chart.
'USER_REFERENCE_TYPE:= {Size=1:40}
'A user-defined definition of the USER_REFERENCE_NUMBER.
'VERSION_NUMBER:= {Size=1:15}
'An identifier that represents the version level assigned to the associated product. It is defined and
'changed by the creators of the product.
'WHERE_WITHIN_SOURCE:= {Size=1:248}
'Specific location with in the information referenced. For a published work, this could include the
'volume of a multi-volume work and the page number(s). For a periodical, it could include volume,
'issue, and page numbers. For a newspaper, it could include a column number and page number. For an
'unpublished source or microfilmed works, this could be a film or sheet number, page number, frame
'number, etc. A census record might have an enumerating district, page number, line number, dwelling
'number, and family number. The data in this field should be in the form of a label and value pair, such
'as Label1: value, Label2: value, with each pair being separated by a comma. For example, Film:
'1234567, Frame: 344, Line: 28.
'XREF:= {Size=1:22}
'Either a pointer or an unique cross-reference identifier. If this element appears before the tag in a
'GEDCOM line, then it is a cross-reference identifier. If it appears after the tag in a GEDCOM line,
'then it is a pointer. The method of delimiting a pointer or cross-reference identifier is to enclose the
'pointer or cross-reference identifier within at signs (@), for example, @I123@. A XREF may not
'begin with a number sign (#). This is to avoid confusion with an escape sequence prefix (@#). The use
'of a colon (:) in the XREF is reserved for creating future network cross-references and the use of an
'exclamation (!) is reserved for intra-record pointers. Uniqueness of the cross-reference identifier is
'required within the transmission file.
'XREF:FAM:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, a fam_record.
'XREF:INDI:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, an individual record.
'XREF:NOTE:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, a note record.
'XREF:OBJE:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, a multimedia object.
'XREF:REPO:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, a repository record.
'65
'XREF:SOUR:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, a SOURce record.
'XREF:SUBM:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, a SUBMitter record.
'XREF:SUBN:= {Size=1:22}
'A pointer to, or a cross-reference identifier of, a SUBmissioN record.
'YEAR:= {Size=3:4}
'A numeric representation of the calendar year in which an event occurred.
'YEAR_GREG:= {Size=3:7}
'[ <NUMBER> | <NUMBER>/<DIGIT><DIGIT> ]
'The slash "/" <DIGIT><DIGIT> a year modifier which shows the possible date alternatives for pre1752 date brought about by a changing the beginning of the year from MAR to JAN in the English
'calendar change of 1752, for example, 15 APR 1699/00. A (B.C.) appended to the <YEAR> indicates
'a date before the birth of Christ. 
'66
'67
'Compatibility with Other GEDCOM Versions
'GEDCOM compatibility is measured on a per tag basis, and depends on how similar the data models are
'for the two different communicating products and on how consistently they understood and complied
'with the GEDCOM Standard. A few inconsistencies in the use of specific tags also crept into different
'releases of the standard itself, due to lack of foresight or inadvertent errors. Within these limits,
'GEDCOM compatible products can exchange data based on GEDCOM 2.0, 3.0, 4.0, and 5.x. Of course,
'newer GEDCOM releases significantly extend the data model for which the newer tag contexts will not
'be supported by older products. Some products have introduced their own variations into their
'GEDCOM form. This will likely provide unique compatibility problems.
'The following are areas in which incompatibilities may arise:
'! Source Structure:
'The SOURce structure was not supported by GEDCOM in versions before 5.x. However, some
'products, prior to GEDCOM 5.x, developed a SOURce structure for citing sources. These structures
'varied from product to product, which affects how source citations are interpreted. Products based on
'5.x GEDCOM, prior to GEDCOM 5.4, may have used the more detailed source structure suggested
'by the previous 5.x versions. Older systems already handling sources will need to be modified.
'GEDCOM 5.x draft products are encouraged to update their programs to The GEDCOM Standard
'5.5 as soon as possible.
'! FAMC Pointer:
'The INDI.FAMC structure has been modified a lot since GEDCOM 4.0. In previous GEDCOM 5.x
'versions the FAMC structure may contain subordinate adoption events and/or LDS sealing to parent
'events. See the compatibility implications concerning the LDS sealing to parent event treated in the
'"LDS Ordinances Dates" in the next paragraph.
'! LDS Ordinance Dates:
'The structure for LDS sealing of child to parent was changed in previous GEDCOM 5.x draft versions
'from the FAM.CHIL.SLGC structure to the INDI.FAMC.SLGC structure. This was to allow access
'to child sealing information without having to follow a pointer to the family record. Personal Ancestral
'File 2.31 writes the sealing date in the FAM.CHIL.SLGC structure but reads this information from
'either format. A new major release of Personal Ancestral File will change to the newer approach.
'GEDCOM 5.4 places the sealing of child to parent event at the same level as all of the other events
'that are subordinate to the INDIvidual tag. If a system is keeping track of which family the individual
'is sealed to, then a FAMC pointer is additionally inserted subordinate to the SLGC event tag that
'points to the sealed-to-family. 
'To accommodate previous GEDCOM imports, systems handling the LDS ordinance events should
'look for the child sealing information in either INDI.SLGC (see LDS_INDIVIDUAL_ORDINANCE
'page 35, 36, INDI.FAMC.SLGC or FAM.CHIL.SLGC structures. Ancestral File exports did not
'68
'separate the temple code from the ordinance date. Ordinance dates down-loaded from Ancestral File
'may contain an ordinance date followed by a two digit temple code rather than a separate temple code
'line. 
'GEDCOM 4.x systems used certain key words as part of the ordinance dates. GEDCOM 5.x
'separated these codes from the dates and specified that they should be values of a subordinate
'STATus tag. Previous GEDCOM 5.x implementations may have implemented this feature using a
'TYPE tag instead of the STATus tag. (See <LDS_(ordinance)_DATE_STATUS>, page 51, 52.)
'! Adoption Events:
'In GEDCOM 5.x, the ADOPtion event was moved from the FAM.CHIL structure to the
'INDI.FAMC.ADOP structure, it also appears in the INDI.ADOP structure. In GEDCOM 5.4 the
'ADOPtion event appears only as an individual event which optionally contains a FAMC pointer to the
'adoptive family. Subordinate to this pointer is another ADOPtion tag which indicates whether the
'HUSB or WIFE in the pointed at family was the adoptive parent (see
'<ADOPTED_BY_WHICH_PARENTS> primitive on page 42). Pedigree navigation is provided only
'by <<CHILD_TO_FAMILY_LINK>> structure found on page 31.
'! Codes in Event Date:
'Some applications, such as Personal Ancestral File, pass key words as part of certain event dates.
'Some of these key words were INFANT, CHILD, STILLBORN, etc. These have to do with being an
'approximate age at an event.
'In this version of GEDCOM, the information has been removed from the date value and specified by
'an <AGE_AT_EVENT> key word value which indicates a descriptive age value at the time of the
'enclosing event. (See <AGE_AT_EVENT>, page 42.) For example:
'1 DEAT
'2 DATE 13 MAY 1984
'2 AGE STILLBORN
'meaning this person died at age approximately 0 days old.
'1 DEAT
'2 DATE 13 MAY 1984
'2 AGE INFANT
'meaning this person died at age less than 1 year old.
'! Multiple Names:
'GEDCOM 5.x requires listing different names in different NAME structures, with the preferred
'instance first, followed by less preferred names. However, Personal Ancestral File and other products
'that only handle one name may use only the last instance of a name from a GEDCOM transmission.
'This causes the preferred name to be dropped when more than one name is present. The same thing
'often happens with other multiple-instance tags when only one instance was expected by the receiving
'system