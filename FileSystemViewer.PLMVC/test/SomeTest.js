

QUnit.module("some module");

QUnit.test( "a basic test exampleee", function( assert ) {
      var value = "hello";
      assert.equal( value, "hello", "We expect value to be hello" );
    });