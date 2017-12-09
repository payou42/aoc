using System;
using System.Text;
using System.Collections.Generic;

namespace _9._Streams
{
    class Input
    {
        public static string RAW = @"{{{{{{<,,!u!!!>'!>},<,!!'!>,<!!!>,<eu!>},<'{<u>},{<!!!>!>!!!>>,{{<!!!!e!>>}}}},{<!!!!!!e{ou,>,<!ua!>},<>},{}},{{{{{{<}!>},<ee!!!oi'!>,<>}}}},{<,!<!a!!!>},<>,<!u'{oi'!!!>'{!!!!!><>},{{{{<}!>'e!>,<,!!!>!>!!!>!!!>,!!{!>},<<u<>}},<{!>},<>}}}}},{{{{<!>,<!>!!!>!!!>a!!!>,<,<}o!>,<o!>>,{{{<!>>},{<!}>}},{}}},{<',<o<'!>,<!>,<!>},<>,{<iu<i!!o}>}},{{{<!!!>!>},<!!io!!<ii!!!>!>},<!>,<a{>}}}},{{<!>},<'!!!!!>'}!>},<i!>},<ii!!!!!u,!!'>,<i<ou{<e!a'!!oo}'e>}},{<!>},<!!u!>,<!!!!!>},<,!!!>},<!!u!!{<>}},{{{{<!!ou!!>}}}},{{{{{{}},{<!>!>eaa!!!>!!u{{!!!>!!!>!!{!!!>!!!>e!>>}}},{{{}}},{<},!!!>i!}}i!>,<!>{!>},<!!!!!>!!!>!!,o!iu>,{{<!>,<!o}!'''<{{!>,<>}}}},{{<!!!!!!!>{!>},<,!!!!!>u!!i!!!i!!}'!>},<>},{<!!<a,}!>},<>}}},{{<i'!!<!!!>}<},!!!>!!e>,<<!>,<'a!>,<!!}!>},<<!e!,ea'uu!e!!>}}},{{{{{{{<>},<u<}!!!!,!>,<!!!>},<>},{{},<!><!!!>!!!>},<o!>},<ue!ou!!!>,!u>}},{<i!>,<!>},<!>,<'{'!!!!!>u!!!!oou}>}}},{{},<a}''!!!!!u<,>}},{{{{{},<!>,<i!!}>}}},{{<o!>'>,<'e,'!>,<>},{{<>},{<e>}},{{{<!>,<!!'a!!!>}!!!>{!!e!!!>!!!>i!>},<<i'>}},{{{<!!!><!!!>!{!!!>eoo,!>'!>!!oa'a'>},{}},{{{{{<o!'!!!>,<!>>},<oo'e,>},<{!>,<!>},<!!o!!!>}!<}!{!!'',!!!!!>>},<!>uo!>,<!>},<!<>},{{{{{<<!!,}u!!'!>e>}},{{<oa{!!!<!!'!>,<!e!!!>a>}}}}},{<<>}}},{{},{<uu<!>,<{{!i!>!!!!!!{'',i'>}}}},{{{<!>!>}!!!>'>}}},{{{<}'u!!{!'!!!!!!!>e!>,!>u'uiu>},{<!!!>!u!!!>}{!!!>!>,<,>}},{<!>!>>,<<'>},{{{<a!!'{i!>},<'{!ee{'a!>},<'!<}>}},{{<!>,<i!!e>},{<!!!!o!ie}!o'!!'ii'!>,<a'>}}}}},{{},{{{<!!!>eia!!!>'a}'!>,<}i>}},{{},<!!!>!>''!!!>,>},{{{<!!e!>,<!>},<!>,<,!>!><!>,<!!'!>},<eu!!!>!!'>},<{{>},<{,,i'{<e!!!>!>},<<<o,a}e{a>}},{{{<u}!!i<!!o!>},<<>}},{<!!,'a<a<!>},<i}!>'!!!!!>>},{{<'!>},<!!<!>},<>},{<ieo!!!!!!{!><!>},<!'>}}},{{{},<{{!>,<!>!!!>},<!>},<!!!>,a'>},{{{<!!!>!><i!'!>,<}!!!>},<!>},<a'!!!>,<!>>}}},{{{}},{<>,{}}}}},{{},{{<!>},<!>!>,<}!>},<!a<{!!!,>},{}}}},{{{{},{{{{<'<!>,<{>}},{{<>},{<,!>a!>!!!>'!!!>'u},!>},<''!!!>},<}!!>}},{{},{<u!>u!!e>,<o''<<o'u!!{!!}i>}}},{{<!>,<'!>au!>!!>}},{<aaa!!o{'!!!>,<!a>}}}},{{{{{{},{{<ii!>},<,e!o,!>},<'!!!>!!!>!>,<}<!>,<!>!>!!'!e>},{}},{{<!>},<!!u!!!>,o!>},<!!!>!}'!!e'!oa!>},<!!<o'>},{{{<!!ue!!!!}>},{}}}}},{{{{{<o<'!!{!!!a{u!,!!a',u!>},<i>,{<{'i>}},{{<!>},<!!'{!!i!!'{e<!>,<!!!>'{!!'>,{}},{<!>!!!!!!e!>i!>,<ueu!>},<<!!'u!!'a>,<{e{}'!!o,!!'a!>},<i!!!>i!{>},{<!>},<,!!'!>},<{'<o{>}},{}},{{<!!!>!!a!>>}}},{{{{},{<!>},<'e,o'}e{!>,<a!{!!!>!!!'!>!>,<>}},<!{o!!!>u!!!>,<!>},<,!o!>!oa,!>,<!>},<!>},<!>,<!>,<!>,<!!!!!>>},{<a!>,<!>},<{!!!!'!!!>e!!,!oi>}},{{{<'>,{<aooo!>,<!>,!!}!>>}}},{<<!!,io>,{<'!>},<!!!!!>},<!!!>i}!!e!><!>,<!!'!>,<!!!!!!!!'!!>}},{}}},{{{{{<!!oei!>!!'a!,a!>>},<,!>!!!!uu!!!!{!!!>,o',!!'e!!!>!>},<!>,<{}>},<{!!!>!oi>},{{},{}}},{{<'!>,<>,<o!a!>,<!u!>{'a!!!!!{!!!>!!!!!!!>!i{>},{{<ia'!!!>>}}},{{<a'}!{'>,<!!{!!<!}ea>},{<}>,{<{e>}},{{<!!!!<!>,<!!<!>},<'o'!>!!!}}o!!<!!!!!!e{>},{<!>},<!!!>}!!!>'!>,<!<aa!!,!!!<a!!!>!!{<!!!>e!!e>}}},{{{{<u'e!!iui!a!>},<'e'!>,<!>,<,!>},<{u!>,<!>>},{}},{{<!>'}'!<!,!!i}ea>},{<!i'!>!o!u!!eeiua!!,!!!>e!!>}},{{<!!!>,<!>,<{'>},{<{,!!e{>,{<!>},<<i',}u!!!!!>},<{'}aa!>,<'>}}}},{{{}},{{{{<e!o!>,<'!>!!!!}{!>,<!!!>,<{!>,<<'!!<>}}}},{{{{{<>}}},{{}}},{{{<'ooeu!!{>,{}},{<}!>,<o!>},<!}oi'!>},<!>!>,<{!>e!>},<>}},{<!>},<!>},<u!!!>{'!!a!!!>,!>},<!>!a,<{u!!e'>,{}}}}},{{{{{<!>,<!!!!!{!{oe'i,o'>,<,ii!>}!!!u>},{{<e!!{u}!!!a!{!>,<,'u,!!!>!>},<>}}},{},{{<{!>!>},<'<!>!!i'!>,<!!'!>},<!>},<!e!!!>}'!!u>},{},{<>}}}},{},{{{<!!!>i!!!>}!!'!>!>!!u,{!'!>},<'!!>}},{<{!!!>!!e!>},<<>}},{{{{<!!''}!!'}>}}},{<,e,!>!!!>,<<>,{{<}}}!'<ia<!!!!!!!>'a''!>},<<o{uu>}}},{{<{<u!!!>'<<e!>},<'}!!!>u,!>!>!u!>,<>},{<'e!>e!a!>!!}iu!>>}}}},{{{{<uau',a>}},{{},{{<>,{}},{{{<'}!u'}a!!}!!!>e!>ua!!!>'>},{{<!!!<!>},<u'e>}}}},{{<,!>,<!a!>u'<!!oe{<u!>},<{>}}},{{<o{<<'!!o<e!!!!!>!!a!!uo'!>},<!!!>ae>}}},{<!!<!>},<!!!>!!a{!!!>!'{>,{{{<e!>},<!>,<e!!!>},<au'u!!i>},{<!!!><e!!!>,<o>}},{{},<'o>}}}},{{{<}}>}}},{{{},{<!{!>}!!!>a!>,<>,{}}},{<i!!,u!>!>!>},<!!!!>}}}}},{{{{{}}},{{<e!!!><>},{{<!>},<i!!!!u!>},<'!>a>}}},{<!>!!!>},<!a<e!>},<!!!ui!!!>e>}},{<'{a!!u!!eaea>},{<!!uu!!!>},<,!!!>'>,{}}}},{{{{}},<'i!i!!!'!>,<}u!!a<!!!!!>},<ai!!!>!!u!>>}}},{{{},{<ie!i!>''{e!>,<!!!>>,{{{{},{{<u!>},<!>i!>},<!!!!'!!a>},{{<!{!>},<!!!>}<a!<<,!>!>!!!>,<<>},{<o!!e'!!!!!>ei<,o,!!,!o,a!a!!!>}<>}}}}},{}}}},{{<>},{<!>i!!!>'!>!>,<>}}},{{{},{{<<!,o>}}}},{{<a!>},<<o,!>o!o!>},<{>,{<,e!>,<>}},{}}}},{{{{<<!!!e!!!>u>},{<!<'!>},<>,{{}}}},{<,!>'iae!!!!{>,<'<u}<>},{{<>},{<!>},<>,<!>a!>,<e}'o!>''!>!>},<!!!>,<>},{<<,!!!>'i!>},<'o!>,<!>,<}!!!!e,'o>,{<{!>e>,{<,'!>}!u!!!>{<{!!!!{!!<}!!!>!!!>'>}}}}},{{},{{<!!}!'!!!>u!>},<o'},>,{{<!>,<,{<!>,<!>!>!!,<!!!>au>}}}},{{<!!!!!>aiui!!}!,''a!!!>'!>u!>!>,<<>}}},{{<!>,<!!oe>},{<!>},<o!'!!!>u!>},<au<>,{}},{}},{{{{<!>},<!!!!{u'<ioo>,<<a!>,<!!!!'<a,<!!!>o!>},<'o!!{}!a>},{{<!!<<!!,}a''i!a'{!>,<u!!'a,>},{{<!!!>a!!!ii!!!!!>e>}}}}},{{{{<iu'!!!!!>,<o'u>},{<'!>,<e!>},<'uueoe!!a!>},<{!>},<o>}}},{{<>},{{<!!!>!!}iaaae!>!!!>!>},<ee>}}}}}},{{{{<uo!u<!>!i!!iea{!>},<}u!ae'!!i>},{<'}!!>}},{{{<{<ioeu}{,,!!'ia>}},{{<u{<a>},<!>!>},<!o{!!!>!>,<!!!>eoo!!ui!>!!{!ii}>},{{{}},{{<},o'!>,<!!!>a}!,'!>},<>},<!>uue>}}},{}},{{<!!!>!!>,<!>},<{!>,<<i,!!!>}oio!>,<>},{<!!!!!>},<}}>,{{<{'>}}}},{{{<'!!!>'o!i!u,i>}},{<!!!>o!>,<{!>},<!!!>,'!>},<'e',{i}!>>,{<!{,!,>}},{{<!>,<'!>>,<u!>},<a!!!!!>!>},<o'!>u}o!!!>'o{>},<ao!'!,!!!>i}!<<i,,>}},{{<i!!!>!!!>,<oe!!''a!!!!'!!o>,<!'e'a<!!!>>},{{{<,'<!><u}!!'ie!!>}},{<i>},{{{<!},!>,<oei!>!!{''!>},<!>},<!!{'e'!!!!!!'!!!>>}}}}}}},{},{{{{{{<!>,<!!!>,<>}}},{{},{{{},{<!>,<'u}}{!>a!!!>!>>}},<u}'{!>,<!!',>},{<eo!ue{!!!>!!!!o''e!i!!!>o,>,{<',!!!>!!!!!>!>,<u,!!}!!!>u!>,<,,!!i!>!>},<>}}},{{{<!!o!!!!!>},<!!'!!!!!>!!!><!!!!!!<o!!u!>},<!>},<!>o<!!!!!>!>!,!!>}},{{{<'!<!!{!>!}'euiii!<ouu!!!>>}},{{{},{<!>},<,<!!{!!!><a!!!>!}!>,<i!ao{'o>}},{{{<!{!>,<!>},<!>,<{o!>e<>},{}}}},{<<!!!>!>>}},{<!>},<u'a!>,<a!!'a}>,{<e!!iiu!!'oe''!!!>u!!!>>}}},{{{},{<!!i!!!e!>!!!><!!!>!>},<}!!'!>,<!>!!!!!>!e>,<!!!>}o!!!>u<!!<!,!<aui!!u!!!ee',!>>},{{<i!>},<,!>,<<<eua>}}},{{{<!>!>a<!>aa'>},<!!!>i'{<{}!>!!!>}e!>,<!!!!!!aio!!!}!!!>},<>},{<!!<!>},<o>,{{},{}}}}}}},{{{{<'!>},<i!,!>},<!!!>'!!a!>,<>},{{<o!>},<<!>,<!>!!!>>}}},{},{<a}'<>,{{<<,i,!!!!!>,>}}}},{{{{{{<',eau'i{!!!>}>}},{{}}},{{<{!,}oi!>>}}},{{{<,>},{<u,e!!a,!>,<!!<!!e!!a!u!!'!>},<e'e'>}},{<ui!e!>},<!!,''}'o}'}e!!!!}!!!!!>u<!>,<>}}},{<!!!>{!!!i!a'a''>},{{{{},<,<u!!!>u!>!oe'!!!!u{!o!,'>},<u!!!!'!'<a<a!o>},{{}},{{{{<!!{!!}!!'oo,!><,}}!!>,{<!,!>'!ia!>,<!!!>{'!>,<o'>}},{<o!!!>,<!a'!!!!!>},<!!!}!!!>e<!!!>'!!!,>}},{{{<!>,<>},<!>},<!!!>!!{e!!o>},<o,!>},<'!>},<a!>!!!uai>}},{},{<<,>}}}},{},{{{},{},{{<,!!!!io!u!>,<!!,!!!>!'!>,<!>>}}},{{{{{<{ou<!>,<>}},{{<!!!<!!!!o!>,<u{!!,!>>},{<!!a'o>}}},{{{<!>i!>>},{},{{<{!!>},<!}!>,<aiiu!>!!<o'}>}}}},{},{{<}a,a!!>},<!>!''!>},<i!!!!!!!!!!,e!>,<,u>}},{{{}},{<!!!>>},{{{<a>,<i'a!>},<!!!>!!!>,<!!!>,<>},{{{<{<{>}}}},{{<!!,!!!!{!ie!!!!}'!>},<ao!>,<>,<!!!>!!i!!!>i!>,<{!>},<}!!!>,<!i!!i<>},{<{>,{<!>,<<!!!!u'u''{!!!!uii>}}},{}}}}}},{{{<!!!>!!{!>}>},{{<!>!,{{>},<!!!>a{u,>},{<!}e!!{!>},<e}i''!!'!>>}},{{{<,!!!>}o,u>}},{{<a!>,<!>,<o!!,!!!!}!<<!>},<'{u'>,{<e!>,<!!!>>,{<!!<,!!'!><>}}},{{<>}}},{{<}}!>,<ao!!!>!!}!o<!!<{ou!>>},{<!!iu!!!>!!!!<!uai>}}},{{{{{<}ia!u!>i<{'!!''u!!!>},<>}},{{<e'!!!ua!!!>!{!<'!!'u>}},{<!>},<'a!>,<!>,<i!!!>,!!!!!>!>,<>,{<!!!>!!!>u!!!>!>,<!>},<!>o!>,<>}}}},{{{{{},<{!!!!'!>,<'!!!>u!!'>},{<!!i<e>}},{<<i{!e{'}u>,{{<<!!!>io''!>},<>},{{<'e!!!><<,!!!>,uei>}}}}},{},{{{<!!!>o!!o,a'!>,<}!!>}},{{<>},{{<'''!!!e!i!u'>,{<{'>}}},{{<}!>!>i!>,<!!'>}}}},{{<,!>,<<!!'!!!>!!e!!<!!!>{uu!!!>>}}},{{{<<!>},<!!!><!>!>!o!!ao>,<!o{!!}!!!>o!>a!!!>},<o!!!>!!'>},{{{{{<ao},!>!i}{!!{!>},<'!>,<>}},{<'<a!!!>'o'!!!>>}},{{<!>i!<!>},<!o<oo!>,<,',!ei}>,{<!>!a{!>,<o<<,,!>,<u!!!>>}},{{},<!>,<!!!>!!!>!!!>},<u<!}o!>{eoeo!!i}e>},{{{{<,i'u!>},<>},{{<!oi>,{{<a!!'a{!>},<{ea!>,<!!ee!>,<'}!!!>,<<>,{{<!!!!!>},<ioa<''a},'!>!>},<>}}},<!!!'>}}}},<!>},<!>},<!!!>'!o{{'!!{}euu<ia!'!>,<o>}}}},{{<!'}u!!i!>},<'{}{}!!u!>i<>}}}},{{<,!>,<!<!!!>}ae!>},<''<!>,<!>},<u>},{<}!!!>{!>},<!!e'}}!!!>!!!>!!!!,o!!<!>},<>}},{<!!!!ai!>,<','!!'>,{<{'!!!!'{!!!>,<!e<!!!>,<!!!'<>}}},{{<!!!>},<o{'u!!!>},<!>},<,'!>}'>,{<!!,!!!>e'!!e'a!>,<oa{!!'>,{<!!!>,!!!>>}}},{{<,<i!!!>!!!!,a!!!>uo>}}}},{{{<!!!!u}!!!!!>}u<a!!!!!>},<>}}}}},{{{<!>'!>a>}},{{<!!<<ii!>oi{!!a'{!!!>!>,<i!!!>>,<ua!>,<}'>},{<o'a!!u<e,!><<!>{!>!>,<>}},{{<e!>>,{<!a<!e{!!!>},<!>!>,<a,u!}e'!!}''>}},{{{{{<a!>,<a'!!'a'}!>>}}}},{}},{{<!!!!{a,,!>,<}}u{ea!!!ia'u>,{<!!!!!ei'!!,!e!>!>},<'!>,<{>}}}},{}}},{{{{{{{{<!>,<,aeu'!!!!'e!>,<,!e'!'!!!>!>,<,,>}},{{{}},{{{<a!>,<!>,<!!!>},<!>},<e!>,<'>,{<{}u!iie!!ai!>,<!>},<<!!u>}}},{<!>,<'!>},<,!>ii,!!}e'!o!>>,{{{},{<<'',!}}o!!!><},}!>},<,>}}}},{{{},<!!!>>}}},{{{<!!'<'!!!!o!>},<>},{<!!!!!u<!>},<!!{'a!>!>!>,<>,{<i!>,<!>,!!!>'>}}},{<!>,<,>,{<}<!!!>e!!!>e'!,u!!>}}}}},{{<<!{,'a!>uia>},{<'{!>,!>,<''!o!>,<!>,<!>},<'!>},<i!>o!!!!'{>}}},{{<>},{<!!i>}}},{{{<ai<!}{!!!>a!{!a!!!!{ue<!!!>o'>},<'!!i'!>},<!>!}{a!>},<!}}!>},<>},{<!>!>},<}!>oo!!!>}}!!!>!>!><!!!<'>},{{<'}!!'i!>},<},a<!>},<!!!>!a!!<{,>,{}},{}}},{{{{<!io,,{!!a!>,<>,<a{'u!!>},{<!>,<!!'a'!!!>!>},<oa!!''!!!!<!i'i>,<,'}>}},{{{<>}}},{{{<!>oo!!},!!!!!><!!{!!!>i!>},<ee,>,{<u!!'!>!!,a'!!!>i<<!>,<'!!!!!>!}i!!>}},{{{{<!>!!e!>,<!!!>>}},<u!!'!!i!>{u'!!!>!>!'!!e'!!!>!>,<!!!!<>}}},{{{},<,!!o}i!>,<,!!<,!>,<eau'!>},<}'!>,<,u>},{<!>,<,!'{ua{!!e!>,<'a!>,<a,'!>},<!e'!,>,<!!'u'u!>},<!u!>!!'a!a'!>,<>}},{{{<!!,<,'u!!i!!'a}'a!>'>},{<'}!!u!>},<oae<ie'>}},{<'!!<!}u!>!!e}>}}}},{{{<!!ua!i!!'a>,{}},{{{<oo!!!!e!!i!'!>!!!>}}!!}!>,<>}},<>},{<{!!o'!!i{ue,!>!!!>!!a,e'!{!i>}},{{{<ui{}!>u!>}<!>'!!!>u{>},{{<!!,<!!{{'>,{}},{{<!!!!u!>{,!i!!u!!!!>,<>},<!>},<!>,<o'eue!>},<e!!e!!<!'e>}}},{{{},<{!!u!>},<!!uu'e,!!!>!!!!e{<!!!!!>},<a!!!!!!!!!!!>>},{<ua!>,}!!'!!!>!i'!>},<<!!!!!!}i>}}},{}},{{<e,u!{',>,<!>{}!>,<!!!>!!!!!>!>},<!!'e>}}},{{<!!!!!>>}}},{{<'!eo!!!>o!!!>},<>,{<o!!'{!>iio!>!>},<>}},{{{<u}'<{{!>,<<e!!!>,<>}}}},{{{<<'>,{{}}},{<{i!!u,!!!>,i!>},<!!!!!>'a>}},{{<u!!!>,<'<!!!>iai<!>},<!>,<!>>}},{{},{{<{!!!i<<u!>!!,,a>}},{<u!o'!!!>'iu!>},<,,!!!>u!>},<'}ai{!>,<>,{<'!>,<>}}}},{{{{},{<,''!{ie}{>}},{{<ea{,!!!><!>,<{<!!'}a,>}},{}},{{<o,,{!!a!>},<!!}}!!a!>!>,<!!!>eo,}!!>,{<u!>{!>,<!!'!!uie<e>}}}}},{{{{{{{<o!,,!!!>u!!',o!!'>}},{{<!>},<!!{{''!!!{!!!>'!>,<!>,<',!!!>},<>},<,e,!!!!oea''!''>}},{{{<,{!>},<,!!!>!>,<!<i'!>,<{!>!!ue!>},<,i'e>,{<}!!aea,}<u>}},{},{<i!>!!!!!>},<>,<}{'<!>},<'<o!>}eu{u<>}},{{{{},{{}},{}},{{<}!>!!a,!>!>,<e>,<,!>i<e}{i!!!>},<{!!!>ee!,<<a>},{<!>'<}!>>,{<>}},{{{<,!!!>},<!>!ie>},{{<!!i!!{aa!!o!!>},<!>},<<o<ie'!>},<!>,<e'o!!!>>}}}},{{{{<!!'!>,<!{<o!!ii'}!>},<!>,e',a{>}},{{{<'!!!!!>,<!>},<!!!>e>}}}},{{<'a>},{{<!!!a!!iuei!!'!!!!!>!!!!!>!a!>,<e!>},<!,<>},<'o!>},<!!!>!>},<<!>,<!!u!}ii<!>e!>,<{ii!!>}},{{<!{!>,<!!!>!!'o'!>{!>,<,{!>,<!!!!!{o>},{}}}},{{{<a!>},<!!!>},<'!>,<iu!!!>,<o,!!!>!>},<u'e!>},<>},{<!}i'!>},<i>,{{<!!a>},{<!>'}e}e!>},<u}!>!!!>!ee!>},<!>>}}},{}},{{{{},{<'<i}e!>!!!!!>a!}ua>}},{{<'!!!>>,{<,!ao{,u!!!>,!!!>i!>,<>,{{{}}}}},{{},{{{{}}},{<{!!!>'!>},<u<<e!!!'!!i!!>}},{<i!!!>!>},<<i!>},<!'!!a'!o>,<<e!!'e!!!!'<a,ou}>}},{}}},{{{<>},{{<!>},<{'!!!!}'!!!>!!!>>}}},{{{{{<!>},<>}}}},<!!!!!>,<u!!},!!u!>},<u!>,<u<>}}},{{<'!>},<!>!i!>,<{ue!!!!>,<o<'e!!a!>},<!!i!>},<{!u{!>},<>},{{<o'!o!!!>!!!>!i!!<e!>,<<i!>},<<u!>!>,<'u>}},{{},<,<o!!!>>}},{{{<a}!!ua'!>!!,e!>,<,>},{<'ee'!!u!>},<!>,<uiuou!!!!>}},{{<{'!>,<!>,<<!>},<>},<<!!!>{!!e!'!{!}iea<!>},<}'aa>}}},{}},{{{},<,!!!>}!>,<o!!!!uo,!>,<!>!!'<!!i>},{{<!>,<!!!>!!i!!!>i,>},{}}}},{{}}},{{{{<!>},<ue!!}>},{<!!'u!>!!<!!{e}>}},<!!!!e>},{{<>}},{}},{{<'!>!!!!!,e'!!<!'a!>},<>,{<!>,<!>>}},{{{<i!!!>!!}{o>}},<>}}},{{{{},{<<}!!!>,<<!!!>},<!!u!>,<!'<!>},<o,!>',i>}},{},{<!>,<}uo,ueo!!!>,<aio!>u!!'a!!!>>}},{<!!!>},<i<!!!>},<!!!>!!!!!!!!,!>!!!!!>!>,<ue>,<<!>{>},{{<}!!e!!!!!,!>},<}!>},<'o'!o<e!u>},{}}},{{<a!!'!!'u!e!>o!!!!!>o,!>!!o!!'!!!>,<ua>},{<!'au{i>,<!!!>,'<e!'!!!!}''oo,!o'>}},{{<!>},<!!o!,{!!!>u!>!!>,{<>}},{{<!>,<,,'!>},<<!>}!!{>},{{{<a!>,<,!!'a!!!>},<'{{io!!!>!!>,{<,!i{!!'!>,<,!>!>,<!!o'!!>,<i!!!'!>'e'!!}!>},<{ae<!>>}}}}},{{{<o!!!>ae<a>}}}}},{{{<e'iieu!>u!!io!!!>!>>,<>},{},{<!!i!>,<uo!e!!!>{u!!,'!!}!!!'>,{<!>e!!!!,iu!!!!!>!>},<!!i}!!!,!>a'{}!>},<>}}},{{{<!}!!!!!>o!!a',>},<!!!>!>!>{'{{!>,<}ioe'!!!!!>!i!!'!>,<!>>},{{},<ua,!!!>!>},<!!{i<<u>},{{{<!'>}},{<!!i!!!>!>,<!>,>,{{<a''ua'e<,!'<!ue!>,<',<,u>},<a!>'>}}}}},{{{{{{{<!>e!!}!>u',i!!!>'!!{!}'}!!'e'>},<!,<<}'!>,<!!ioaa>}}},{{<'>,<<{!!!>e'<e!!!>!!!>!!}'i!>,<>},{<<a'e!>eu!>a}!!!>u!!!>!!>,{{{{<!!!o>}}}}},{{{},{<!!!>ia!{!!!>{>}}}}},{{{{{<!!iu'e!!!>>,<!!!>{!!o!!o'!>},<io!>,<{!>,<!!<!!'ii>}},{<'!!!!io>}}},{{<<oo,'a'i}!>,<o!!!>,<>},{<i!>,<!!!>>,{{{{<!!!>'o!!!!!!u,u!>''!!a!>iu!!{!!>},{<a!>,<ae!!a''!,'!!'{!a!>!,o>}},{}},{<i'a!!!!'!i!!!>'}e!!e}!!,!'!>},<{}!!!>!e>}}}},{{<!!u!>},<{,!!a!!!>},<>,{<'!>'!>!>'!!'!}>}},{{<!>,<aoua'!!!i{,eo,!>},<{i!>,<a,>},<!!!>!>},<!u!!!>,<!>},<ui!!!>,<!>},<o!>},<eo{'{>}}}},{{{{<!>,<u!!!>},<>},{}},{<!!,!>,<'}!>,<>,<,!!}{!!!e!!,>},{{{<<!<!!<!!!>!>!>,<!>'!a!!!!'!>oai>},<},!!!>aiu!>o>}}},{{<!<',!!!!a!>,!!o!<!!{e!!{>,<!!{}i<>},{{},{<!>},<i{!>'{!>},<<i!!!!!!!>''<}>,{<,!!!!>}}}},{{{<!!!!!>,{a',<!!!!,}!!!>o!>},<!!!!!><<oe>},{<!<'{}}}!!!>!,a>}},{{<!>,<{!!o'u!<!!!>a<!>},<>}},{{<a!!'>,{<,>}},<>}},{}},{{{<uu!!o!>},<oi!>},<!><,',!!i>,{<>}},{<!!u!!''''!!!!o>,{}},{{<'!!ai'!>,<{!>},<'!!!!!!>}}}},{{},{{{{<,!<,,'a,!>},<iuo!>,<}''{u'>},{<!!ai>}},{<!>},<,!>},<!!!!!>}{',!>,<o''}{,>}},{},{{}}},{<!<!>!!!>}!>},<!!!!!!}o!>},<a'!!!a}!!}'!>!>},<}>,{<,!>'a'<!>!!!!e'>}}}},{{{{<!>i>,<!>,<{oi'e,'i!>!<>},{{<!>},<!>,<,au>},<,<!''o!!i!>},<u{}'aaea<>},{{},<a{!!'!>,<eo<!!eu!>},<>}}}}},{{{{<o!>,<!!!!!!<!}!>},<a!>},<>}},{{<u{!!i!>},<e,}!!!>'!!!'{>},{<'<!e{!,!>,<!,!!!>e!!a!!{e>}},{<>}},{{{{<!!!>u!!e!>,<a!>},<e,!!<>}},{{{},{{<!>{!!!>>},{}},{{{{<!>}!{i!!,!}!!!'!>!}!!!>},<,a>},{}},{<!>},<o<!!'e!!'a',!>},<'e!>},<}o>}}}}},{{{<{!!!a!!!!!!e{,o>},{{},{<'!u!>!>},<!>},<,'!!!!{o}!!!e>}},{{{<>,<i!!!>!>,<!!!!!>},<'!><<,>},{{<{,!>,!>!>},<<,'!!}!>e>},{<'!>,<!uo<!!i!'{>}}},{{{<u!!'!>,<u,<i!!!>{>},{<u!!!>,<{!>,<i{o!>!!i'i!>!>,<'o!!!>!i>}}},{{{},<{'e!>!!!>,,!>,<!!u!!!>!!!!!>,!!!>},<<<<!i>},{{<o!o<!>},<}!!!>a!>},<}>},{{<{!>},<u!!!>},<{i}!!'>}}}}}},{{{{{<ua!>,<!>!!!>!>,<!!!>!>},<!<!'o{!!!>!>,<oa{>}}},<,u!>,<!}!{'}!!!>!'!!!>,<!!!>{!!!>!!!>!!!>!!o!!!>>}},{}}},{}},{{{{{<u'!>},<''>},<!!<''e!!o>},{<u{<!>i',e{!>},<!>,<!>,<!oou<ai!!}>}},{}},{{{{{<!{e'i,'}'{e>,{<!!!><'!>,{!!!>!!}!!!>!!!>>}}},{<'!!!>!>,<a}!!!a'!}{u>}},{{{<'!>}i!iu!!!!!>!>},<,!>i'!!!>>},{<>}},{{<!a!!!>},<!!!u{>}}}},{{<}!!o<!>},<<<!>},<!!!>a!>,<!!!>'!e,{,'!!<>},<u!!!>a'<''iu!>!!!>'!!!>u>}}},{{{{{{{<'!!!><eouie!o!!e'>},<!>,<<!!!!ua!!!!',a}ee!>,<!>},<i!!{>},{<!u!!'!!!>,u!>},<!,,!!!>,u{!>,<>,{<auo!!e>}}}}},{{{{{},{<'!>}!>},<!>}i{!!u{!!{<}'!>},<>}},{}},{{<!i!>,<{a'!!!>!!!>>},<!!!>!>!>},<!{!o!>eo!>},<'!>},<a>},{{<!>},<!>,<'a,>},<a!>,<!!a!>!!}a'!>,<!>},<i<!>,<o<e''>}}}},{{{{},{<!!i!>!>,<!!!!!>!>!>},<'!>}'!!>}},{},{<,!>,<,a!>,<!!!!!>!!!>e}e!>},<{o{'!!,<>,{<,o!!!<ie!>,<u!>},<!!!>,u>}}},{{<o!!!>},<>,{}},<u!!e!!!>!!!>i!}!>},<'!>,<!>},<}!>},<!>,<!o'!oe>},{{<a}!>},<a!!!!!>!>!>}iii!!!>},<!!!>},<!>,<!>,<}!!{uo>}}},{{{<!>},<!!<!!o<{!!!>aa!!,>},{{<'!>},<e!!!!!!!>e!!oe'!,!!!>''!!!>!}i>,{{<<!!>},{<'<'!!ee'!>},<!u!!!>},<!>},<!!uu!!!>>}}},<oaa!!!!{!>!>},<!}<!!!!e>}},{<'<>,{<!!e'!!!!'!a{{euoe!'!>},<!>},<!!o>}},{{<}!!e,{!>,<{!!ua!>},<!!!!<{>,{{<!e'{!>>},{}}},<e!!<>}}}},{{{{{}},{<{u!!!>}!'<o'e,!>,<!!!>,!!!>{'>,{<!>}>,{{{}}}}}},{{},{{}},{<!!i<}!!<a!'!>},<'!!!!!><!!!>},<>}},{{{{{{<<!!<'!!!!!>!uo>},{},{<e<!>,<e!!'e!}}o<i>,{}}},{{}}},{},{{{{{<'!!!><oo'ei'!!,e!!e'!!e!a!!!>'<>,<e!>,<!!}{o!!{i}ioi,'!>{!!!>,<!>e,>},{<!!!>ua}!!!!eou!!{!!>}},{<<>,{<{}ei{!>},<'u}}'>}},{{<!!!>,<!!!>!!!!!>,<a'e!>u<!!'}!>!>,<i{!!!>>},{{<}!}u!>},<!!{!>},<o!!!>!!!>>}}}},{{},{}},{{{{{<>},<!>,<!>,<'o'eu'!>,<'!>,<!!{{o<>}},{{{<,!!!{!'}!!<!>,!!!!{!>,<!aiue!>,<>},<!!!o!'{!<'{!!!>'!>'a!!!>,<'!>},<>}}},{{<,}!!i'a!!'u!>},<!!!>},<,'}!<!!!>}<!'>,{<!u!!u!!!><a!>,<'!!'!>},<!!'!!!>,>}},{{<}i'!>},<'ui>},{{}},{<'{!!io!,u!!!!!!'u!>,<>,<!}uea!>{!>,<e!'a,'oo>}}}}},{{{<o!>},<i!>},<,i!,!>,<!!!>!>},<o>}},{<!<!!}!!{'<i'a!!!!!!!>,<}{!!!>},<>}},{{{{},{}}},{{},{<!>,<!!>,<!!!>},<'!!!><>}}}},{{{<!!oaa''''!!<a!,!>},<!!eie!!!>,<u!>,<!>,<>}},{{<{a!!!>}!!,!>,<ia!>o!>,<>},{}},{<!>,<!!!>,<'!!!>!!i}!!!!o!>,<<<!>,<!>,<!<>,{{<!>,<'{}i!>,<'aaaa}o,<',!!!>,<>}}}}},{{{<ue!>,!>},<!>e}'>}},{<!o!!'!!{}!>'!i!!a'o!>},<e>,{<i{a!!!!!>'e!>}{<'!>,<!>},<!,!>,<'e>}},{{<!>},<<}!>!!!!o}'!>,<u}!!!>!!!>!>},<!!!>,<!!!!}!!!>!>,<!,>,{{<!}e!>,<!'!!!>}i>}}}}},{{<i!!!>!!>,<'{!!!!o'!!oe!!!<i>},{{{<ia,!!!!eo!>},<>},{<!!!>!>!>},<!{a{>}},{{{{<!!i!!<a,'o!!ouao>}},{<}!!!>ooo{!!!>,<!>},<!!!>!!<o}!>,<!!'e}{,>}},{<>,{<,!oo<!a!>,<>}},{<u!<u>}}},{<,!!!>,<!!{!!!>e!>,<u'!>,<!>,<!!!>'e'<>,<!!!>}!!!>aa!'!!{!>u{!>,<}!!!>!!o{!a>}}},{{{{},{{},<!!u!i>},{{<'!!!>u'!>},<!!!>}'o,!>},<ai}}!>,<>},{{}}}},{{<!}euo!!!o!>,<<!!!>e!}o{>,<!>},<!>,<!>'!!!'e!!!>>},{{<<<!<{!>>},<!o<>},{{{<e!a!!!!!>'{!!!>},<!'!>!aau!!!>'!<>,<u}{}!!u!!!>'!!!a'!!!>>},{}},{{<'{'!>!'}i!!!>eu'i}>}},{<!!!>,<!u'i!>!!!>!>},<!>,<u!!!',{!!!>a}!>,<!!!>},<{>}}}},{{{{<!!'!>},<>},<!>},<'<!>,<>},{<!!''!>u!!<<!!i}{eu!>,<'!>,<,>}},{<}u>},{}}},{{<o}i!!aio!!!>u!!{!>!!}>,{{{{{<'>},{{<o>},{<!!,e!>,<i!!ie}!>!!!>,<o>}}},{<<!!<<o<ui{!!!>!!}ee!!!>>}},{<,!!!}!>},<!!!!!,>}},{<!>},<u,>}}},{{},{{},{<!>,<!>},<!>}'{!>!>},<>}}},{{<!!!>{>}}},{{{<iiu>}},{{}}}}},{{{}},{{{<!!!!!>e<!!!!!>'{'!>},<>},{{<,a!!!<!!!>i!!aoo,>}}},{{{<iea!>},<!>ao!!!!'<!>,<!!!>!!!>o'!!!e!>,<>}},{<!!!>a}>},{<,oeo>}},{}}},{{{{}},{<,!>},<>},{{<a}au<}o,e}!>,<}a<'!!!>>,{<!!!>!a!{e!!i!!,,,}}{o}ue},>}}}},{{<,!>},<u'!!e!>,<a<!!u!!!<'a>},{{<}<<',ou!!!,}u!>},<!>,<e<!>},<u!!!>>}},{{},{<a'!>},<<au}{!!}!>},<}}u}{>}}}}},{{{{{<!!!>!>},<i'oe'{!!!!oa>,<!!!>!>o!>!>},<'!>!!!!oe!a!{o!!!>}u>},{{<!>,<!<{!>,<!>i!!!>>,{<e,','>}},{{},{}}}},{{{<!!!>!{a>,{<'!>},<!>i'!>!>},<!!!>!>,<o!>u!e>}},{{<>}},{<<}}!>},<!o!>,<{<>,{}}},{}},{{{{{<!>},<>}},<!>,<'i''!!!>!>''u!>!!!>!!!>>},{<o,!!!>},<!>!>},<}u'<!<!'!'!>!!!!u<i>,{<!!!ue'!!{!!!<!'!>,<!a>}},{{<!e!!>}}},{{{{{<<'!>},<,!!!!!!oua{!>,<!!!>>},{{<i{i!>,<i!{i!>,!>},<'!>},<!!!>},<!>,<i!!'{>,<o<{,!!!>!>!!!>!!!>!!!>>},{{{<<!!!!!>},<!!!>,<>},<oa}'i,''a!'e!!!!!o!!!>>}},{{<iu!><<<<i!>,<'<>}}},{{<!!}!>},<'!!ei!!aa},!>o!>},<!!}!!'!'!!>}}},{{<!>e!>},<!>},<!>a'>},{}},{}}}},{}}},{{{<>}},{<<'!>},<!!!>a!!!!}!!!!!>!>,<'e<!!''o'{u!a>},{{<,e!a!!!!,i!!!>},<'!>},<}>}}},{{<u!!!>{!!!>!!'!>,<,!!!>!>},<!>},<!>,<!'!>,<'!!'<!!!>>},{{{<!!!>!!{}>},{<!>,'a!>!>},<!!o>,<!>},<!!i!!!>!>},<!!!>},<'''ua<!>},<a!!!>'!!{>}},{<!!'e>,{<!>,e!>},<<'!!!!!!e!>!!'u'!!!!'ea{>}}},{{<!!!>},<'i'!a,!a!<<'<!>},<<au>},{}}}}}},{{{{{<,!>},<!>,<!<ua!>uu!!aoa}>},{<>,{<',iee!!'>}},{{{<,!>,<!!!>>,<!>i!>{i>},{<'!!!',,i,>}},{}}},{{{<>,{}}},{{},{{{<},ie,{!'>,<e!!e!>},<!a!>},<>}}},{<!!!>},<!!!>!>},<o'e!>,<!>e!>},<{!><o!!i>,{{<a,oiuu!!!>},<>}}}}},{{{<!ai!>,<!>},<>},{{<!!!>!!!>a{!>},<}{uiau,!,>,<a!>,<!>},<,!>o!>},<!!!ia!>,<!!!><!>},<}!<,oo>},<>},{}},{{<''e!>,<a<!!!>!!,>,{{<!>,<!>},<!>,<!!a!!!!!>!!!!!>!>,<!!!!!>},<!>,<!}!!'!!!!!><>},{{{<!!!!!>a>},{<{!ou>}}}}}},{<!!!>>,<'!>',uu{!!!!e!>,<>}}},{{{},{{{{{<o>},{<!><!>'{!>},<u!>,''i!>},<!!u!!!>},<!'>}},{{{<!!i}!>!>'}!>,<!!{io!>,<oi!!i!!o!>,<!!!>},<>,<i<!!o!!!u!>,<!>,<u!!!>a<!!!>,}i!!<!>},<>}}},{<!>{!!!>!>,<!!'>}}},{{{{<{}{!>},<!!!>a>}},{}}},{{<<!>,<!!'oe!>},<!>},<!!!!e'!>},<e,!!i>},<!!!>,<oe!!{!!''>}},{<!!!eueu',!!!>!!!>},<!!{!!u!>,<>}},{{{{<!>!!!>'!!{!!!>!a!!!>!>,<'i>}},{<o!}'aei<!>},<'au!a!>,<,i'>}},{<!!!>!!a}'!'!>,<'o!!!!i<>,<!>!!!>!>},<!<<!}o!>'<!>},<,!>,<!!!!!>!>>},{{{},<'u'<ui!>},<ia!>,<,o!!!>{!>},<e>}}},{{{<!>'!>,<e!u!>,'o!!!>a{!!<!!u!}!>,<>,{{<}!>'a{aa{{>,{{<!>},<!!a<e!!!>!<!!!!!!!!o!>,<!>},<!!!!!>!!o>}}},{{<e!!!!!a!>'e}iee{u>}}}}},{{{<>}},{{<>}},{{{<e!>},<eu!>'!'!>a>}}}}},{{<'!>,<!!!>'!>'e'>}}},{{{{},{<}i!'eo!>'ui{!>},<<u!>''>},{{<!!!>u!>!>,<!!!!'!!'!!{!!!>!'!!'!!u>},<,}!>,<>}},{{{{<eo!>>,{{},{}}},{{{},{}},{{{{},{}}},{<',u,!!!!!!!!!!!>{}!!i!>,<}<!!!!!>}>}}},{{<'!!!,!!!>}!!!>!>,<!>,<}'!!a!!>}}},{{{<!!!a!}uo'!!!>ii!!!!<!>,<'u'>},{}},{{{<<!!i<!u!o,'!>!e}}!>>}},{<a!!oo{!>,<e!!!!!!!>>,{}}},{<ao!>},<!>}oa!!!>>,{<}!>!{!>,<a!!!>,<e},'!!!>},<!i!!a!>,<>,{<a,i{!>,<'!>},<}!!a'!!{'>}}}},{<aai>,<'o!<u,!!!!!>{}a>}},{<>},{{{}},{{<!!!{!>,<u!!!>'!>},<'!}!!auo>},{{}}},{{}}}},{{{},{{{<!,!>'a!<aea!>}{<u!'!!{>},<,!!{!>},<a!>'!oa!>,<!!!>>}},{{},{<!>!!a<>}}},{{}},{{{{},{<{!!!>!!{!>!!!>!!e>,{{<,!><!!e!!!>!!!>!!,}>}}},{<!>a,<<'<'!>a!!!e>,{<!>!!'}!'!!!>'!!!!,!!{!ea!!!!!!{'!>},<{{{>}}},{{{<>}},<!!'!o!>},<a!!!>oe{!!''!>,u!>,<!!!>},<a!!!>e>}}},{{{<a>,{<>}}},{{{<!>,<}<!o>},<!!!{'{!!!!!>!!ou}e!}},<!!u!!!>{u>},{{}}},{{{},<a!!!>!i!!{!!!}!!!>!>!!i!!'!o!>oia{e}>},{<a!!>,{}},{{{}},{<!!!>e'!!!>!!!>ui!!'u!>},<!!}io!!!>>},{}}}}}},{{{<!!'e!!!>u!>,<!,a!!,o!>i,!>},<u'>},{<}>}},{{}}}}},{{{{<!!ue!}!!o!!!!!>!,i'!!<'<!>uee>}},{{<!!!>!>,<',ai!!u!!!!!>!'>},<e'!>,<!>,<!e{!ue'}}aauo!!o>},{{<!!!>!!!!!>!!!>!!!>!!!>!!!,!>,<,>,{{}}}}}},{{{{<'i'a{!!!>!,i!u!>!>},<u!!<a!!!!!}u>}},{<{!!'ii!>},<<!!!>},<!!!>u!><o!!!}>,<u!>,<!!<}!!!>{a'{!>,<{!>,<>}},{{{{{},<a!!!{i'!!!}>},{{},<u!!!!!>!>,<!>'!a'u!>},<>}},{{}}},{{},<''!a!u>},{<>,{<ui!>,<>}}},{{{{}}},{{<e,{ao!>},<>}}},{{<!>,<>,{}},{{{<'!>,<i!!!>!e'u'!>},<{'>},<{uiaa}!!!{!>oo!!>}},{{<!>,<e'i}'uo'!,!!'>},<oau!>'<i!!!!!>!!!>!'{!i>}}}}}";
    }
}